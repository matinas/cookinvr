using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoringAudio_v2 : MonoBehaviour {

	public AudioClip scoringClip;
	public AudioClip finalScoreClip;

	// Use this for initialization
	void Awake()
	{
		GetComponent<AudioSource>().volume = 0.3f;

		GetComponent<ScoringController_v2>().onScorePointSpawn += PlayScorePoint;
		GetComponent<ScoringController_v2>().onFinalScoreSpawn += PlayFinalScore;
	}

	void OnDestroy()
	{
		GetComponent<ScoringController_v2>().onScorePointSpawn -= PlayScorePoint;
		GetComponent<ScoringController_v2>().onFinalScoreSpawn -= PlayFinalScore;
	}
	
	void PlayScorePoint()
	{
		GetComponent<AudioSource>().PlayOneShot(scoringClip,0.35f);
	}

	void PlayFinalScore()
	{
		GetComponent<AudioSource>().PlayOneShot(finalScoreClip, 0.4f);
	}
}
