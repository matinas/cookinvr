using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager_v2 : MonoBehaviour {

	public TextMeshPro timeDisplay;

	public static TimerManager_v2 instance = null;

	private Timer_v2 timer;

	void Awake()
	{
		if (instance == null)			// If instance do not exist, set instance to this
			instance = this;
		else
			if (instance != this)		// If instance already exists and it's not this...
				Destroy(gameObject);	// Destroy this. This enforces the Singleton pattern (there can only ever be one instance)

		timer = GetComponent<Timer_v2>();
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeDisplay.text = timer.ToString();
	}
}
