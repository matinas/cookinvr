using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay_v2 : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		ScoreManager_v2.onScoreChange += HandleScoreChange;	
	}
	
	void HandleScoreChange(int score)
	{
		GetComponent<TextMeshPro>().text = score.ToString();
	}
}
