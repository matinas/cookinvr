using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ScoreData
{
	public int finalScore;
	public List<int> partialScorings;

	public ScoreData(int fs, List<int> ps)
	{
		finalScore = fs;
		partialScorings = ps;
	}

	public static ScoreData operator+ (ScoreData sd1, ScoreData sd2)
	{
		ScoreData sd = new ScoreData(0, new List<int>());

		sd.finalScore = sd1.finalScore + sd2.finalScore;
		sd.partialScorings.AddRange(sd1.partialScorings);
		sd.partialScorings.AddRange(sd2.partialScorings);

		return sd;
	}

	public override string ToString()
    {
		string str = "Partial scorings: [";
		for (int i=0; i < partialScorings.Count; i++)
		{
			str += (i == partialScorings.Count-1) ? partialScorings[i].ToString() : partialScorings[i].ToString() + ", ";
		}
		str += "]";

		str += ", Final score: " + finalScore;

        return str;
    }

	public static ScoreData Max(ScoreData sd1, ScoreData sd2)
	{
		if (sd1.finalScore > sd2.finalScore)
			return sd1;
		else
			return sd2;
	}
}

public class ScoreManager_v2 : MonoBehaviour {

	public delegate void OnScoreChange(int score);
	public delegate void OnNewScoring(ScoreData scoreData);

	public static event OnScoreChange onScoreChange;

	public static event OnNewScoring onNewScoring;

	public int correctScore, wrongIngrPenalty;

	public static ScoreManager_v2 instance = null;

	public int currentScore {get; private set; }

	void Awake()
	{
		if (instance == null)			// If instance do not exist, set instance to this
			instance = this;
		else
			if (instance != this)		// If instance already exists and it's not this...
				Destroy(gameObject);	// Destroy this. This enforces the Singleton pattern (there can only ever be one instance)

		// Sets this object to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		currentScore = 0;
	}

	public ScoreData ProcessScore(List<string> ings, Recipe_v2 r)
	{
		List<string> recipeIngs = new List<string>(r.ingredients);

		ScoreData score = CalculateScore(ings,recipeIngs);
		//ScoreData score = new ScoreData(50, new List<int>(new int[] {10,20,30,40,50,60,70,80,90,100}));

		Debug.Log("[SCORE] " + score.ToString());

		UpdateAndNotify(score);

		return score;
	}

	void UpdateAndNotify(ScoreData score)
	{
		// Notify the ScoringController so it can display the new scoring
		if (onNewScoring != null)
			onNewScoring.Invoke(score);
		
		currentScore = currentScore + score.finalScore;

		// Update the current score and notify the display so it can update the score text
		if (onScoreChange != null)
			onScoreChange.Invoke(currentScore);
	}

	public ScoreData CalculateScore(List<string> ings, List<string> correctIngs)
	{
		// Debug.Log("Score calculation started...");

		ScoreData score = new ScoreData(0, new List<int>());
		
		int penalty = 0;

		if (ings.Count == 0)
		{
			foreach (string ci in correctIngs)
				penalty += wrongIngrPenalty;

			if (penalty != 0)
			{
				score.partialScorings.Add(penalty);
				score.finalScore += penalty;
			}

			// Debug.Log("[Empty Ings] Score returned");

			return score;
		}
		else if (correctIngs.Count == 0)
		{
			foreach (string i in ings)
				penalty += wrongIngrPenalty;

			score.partialScorings.Add(penalty);
			score.finalScore += penalty;

			// Debug.Log("[Empty CorrectIngs] Score returned");;

			return score;
		}
		else
		{
			if (ings[0] == correctIngs[0])
			{
				// Debug.Log("[Ingredient match detected]");

				score.partialScorings.Add(correctScore);
				score.finalScore += correctScore;

				ings.RemoveAt(0);
				correctIngs.RemoveAt(0);

				// Debug.Log("[Ingredient match detected] Before recursive call");

				ScoreData sd = CalculateScore(ings, correctIngs);

				// Debug.Log("[Ingredient match detected] After recursive call");
				// Debug.Log("[Ingredient match detected] Current score: " + score.finalScore);

				score += sd;

				// Debug.Log("[Ingredient match detected] Score after adding: " + sdResult.finalScore);
				// Debug.Log("[Ingredient match detected] New score calculated");

				return score;
			}
			else
			{
				List<string> tmpIngs = new List<string>(ings);
				List<string> tmpCorrectIngs = new List<string>(correctIngs);

				tmpIngs.RemoveAt(0);
				ScoreData sd1 = CalculateScore(tmpIngs, correctIngs);

				tmpCorrectIngs.RemoveAt(0);
				ScoreData sd2 = CalculateScore(ings, tmpCorrectIngs);

				ScoreData maxSD = ScoreData.Max(sd1,sd2);
				score.finalScore += maxSD.finalScore + wrongIngrPenalty;
				score.partialScorings.Add(wrongIngrPenalty);
				score.partialScorings.AddRange(maxSD.partialScorings);

				return score;
			}
		}
	}
}
