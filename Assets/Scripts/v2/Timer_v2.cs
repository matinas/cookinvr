using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_v2 : MonoBehaviour {

	public delegate void OnTimerComplete();
	public delegate void OnTimerTick();

	public event OnTimerComplete onTimerComplete;
	public event OnTimerTick onTimerTick;

	public int initMins;
	public int initSecs;

	public int currentMins {get; private set;}
	public int currentSecs {get; private set;}

	private bool isRunning;

	private float lasttsec, tsec;

	// Use this for initialization
	void Start ()
	{
		currentMins = initMins;
		currentSecs = initSecs;

		isRunning = true;
		lasttsec = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isRunning)
		{
			int rtoi = Mathf.FloorToInt(Time.time);

			tsec = rtoi % 60;
			if (lasttsec != tsec)
			{
				if (--currentSecs <= 0)
				{
					if (--currentMins < 0)
					{
						currentMins = 0;
						onTimerComplete.Invoke();
						isRunning = false;
						// Reset();
					}
					else
						currentSecs = 59;
				}

				lasttsec = tsec;
				onTimerTick.Invoke();
			}
		}
	}

	public override string ToString()
	{
		string minStr = currentMins >= 10 ? currentMins.ToString() : "0" + currentMins.ToString();
		string secStr = currentSecs >= 10 ? currentSecs.ToString() : "0" + currentSecs.ToString();

		string timeStr = minStr + ":" + secStr;

		return timeStr;
	}

	void Reset()
	{
		currentMins = initMins;
		currentSecs = initSecs;
	}
}
