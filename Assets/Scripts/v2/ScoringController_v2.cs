using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringController_v2 : MonoBehaviour {

	public delegate void OnScorePointSpawn();
	public delegate void OnFinalScoreSpawn();

	public event OnScorePointSpawn onScorePointSpawn;
	public event OnScorePointSpawn onFinalScoreSpawn;

	public GameObject scorePointPrefab;

	public Transform scoreSpawnPoint;

	public AnimationClip partialScoreAnim, finalScoreAnim;

	private GameObject scoreIndicator;
	private GameObject scorePoint;

	// Use this for initialization
	void Awake()
	{
		scoreIndicator = GameObject.Instantiate(scorePointPrefab, scoreSpawnPoint.position, scoreSpawnPoint.rotation);
		scoreIndicator.transform.parent = scoreSpawnPoint.transform;
		scoreIndicator.SetActive(false);

		scorePoint = scoreIndicator.GetComponentInChildren<Animation>().gameObject;
		Animation anim = scorePoint.GetComponent<Animation>();

		anim.AddClip(finalScoreAnim,"FinalScore");
		anim.AddClip(partialScoreAnim,"PartialScore");

		ScoreManager_v2.onNewScoring += ShowScoring;
	}
	
	void ShowScoring(ScoreData score)
	{
		Debug.Log("Score will be shown!"); 

		ResetScorePoint();
		StartCoroutine(SpawnScores(score));
	}

	void ResetScorePoint()
	{
		scorePoint.transform.position = Vector3.zero;
		scorePoint.transform.rotation = Quaternion.identity;
		scorePoint.transform.localScale = Vector3.one;
	}

	IEnumerator SpawnScores(ScoreData score)
	{
		scoreIndicator.SetActive(true);

		Animation anim = scorePoint.GetComponent<Animation>();
		TextMeshPro scoreText = scorePoint.GetComponentInChildren<TextMeshPro>();

		Debug.Log("[Scoring Controller] Llegaron " + score.partialScorings.Count + " scores al ScoringController");
		Debug.Log("[Scoring Controller] El final score que llego es " + score.finalScore);

		// Spawn partial scores

		foreach (int s in score.partialScorings)
		{
			scoreText.text = ScoreString(s);
			anim.clip = partialScoreAnim;
			anim.Play("PartialScore");

			if (onScorePointSpawn != null)
				onScorePointSpawn.Invoke();

			yield return new WaitForSeconds(anim.clip.length);
		}

		// Spawn final score

		Debug.Log("[Scoring Controller] Llegaron " + score.partialScorings.Count + " scores al ScoringController");

		scoreText.text = ScoreString(score.finalScore);
		anim.clip = finalScoreAnim;
		anim.Play("FinalScore");

		if (onFinalScoreSpawn != null)
			onFinalScoreSpawn.Invoke();

		yield return new WaitForSeconds(anim.clip.length);

		scoreIndicator.SetActive(false);
	}

	string ScoreString(int s)
	{
		string scoreStr = s > 0 ? "+" + s.ToString() : "-" + Mathf.Abs(s).ToString();

		return scoreStr;
	}
}
