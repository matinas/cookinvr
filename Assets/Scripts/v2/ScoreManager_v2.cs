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
}

public class ScoreManager_v2 : MonoBehaviour {

	public delegate void OnScoreChange(int score);
	public delegate void OnNewScoring(ScoreData scoreData);

	public static event OnScoreChange onScoreChange;

	public static event OnNewScoring onNewScoring;

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

	public void AddScore(ScoreData scoreData)
	{
		// Notify the ScoringController so it can display the new scoring
		if (onNewScoring != null)
			onNewScoring.Invoke(scoreData);
		
		currentScore = currentScore + scoreData.finalScore;

		// Update the current score and notify the display so it can update the score text
		if (onScoreChange != null)
			onScoreChange.Invoke(currentScore);

		Debug.Log("Current score is " + currentScore);
	}
}
