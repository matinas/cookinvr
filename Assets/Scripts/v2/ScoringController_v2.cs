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

	public Color scorePointColor, penaltyPointColor;

	public AnimationClip partialScoreAnim, finalScoreAnim;

	private GameObject scoreIndicator;
	private GameObject scorePoint;

	private Animation anim;
	private TextMeshPro scoreText;

	// Use this for initialization
	void Awake()
	{
		scoreIndicator = GameObject.Instantiate(scorePointPrefab, scoreSpawnPoint.position, scoreSpawnPoint.rotation);
		scoreIndicator.transform.parent = scoreSpawnPoint.transform;
		scoreIndicator.SetActive(false);

		scorePoint = scoreIndicator.GetComponentInChildren<Animation>().gameObject;
		anim = scorePoint.GetComponent<Animation>();

		anim.AddClip(finalScoreAnim,"FinalScore");
		anim.AddClip(partialScoreAnim,"PartialScore");

		scoreText = scorePoint.GetComponentInChildren<TextMeshPro>();

		ScoreManager_v2.onNewScoring += ShowScoring;
	}

	void OnDestroy()
	{
		ScoreManager_v2.onNewScoring -= ShowScoring;
	}
	
	void ShowScoring(ScoreData score)
	{
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

		// Spawn partial scores

		anim.clip = partialScoreAnim;
		foreach (int s in score.partialScorings)
		{
			FormatScoreText(scoreText, s);
			anim.Stop();
			anim.Play("PartialScore");

			if (onScorePointSpawn != null)
				onScorePointSpawn.Invoke();

			yield return new WaitForSeconds(anim.clip.length);
		}

		// Spawn final score

		FormatScoreText(scoreText, score.finalScore);
		anim.clip = finalScoreAnim;
		anim.Stop();
		anim.Play("FinalScore");

		if (onFinalScoreSpawn != null)
			onFinalScoreSpawn.Invoke();

		yield return new WaitForSeconds(anim.clip.length);

		scoreIndicator.SetActive(false);
	}

	void FormatScoreText(TextMeshPro scoreText, int s)
	{
		if (s >= 0)
		{
			scoreText.text = "+" + s.ToString();
			scoreText.color = scorePointColor;
			scoreText.GetComponent<Renderer>().material.SetColor("_GlowColor",scorePointColor);
		}
		else
		{
			scoreText.text = "-" + Mathf.Abs(s).ToString();
			scoreText.color = penaltyPointColor;
			scoreText.GetComponent<Renderer>().material.SetColor("_GlowColor",penaltyPointColor);
		}
	}
}
