using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Timer_v2))]
public class TimerAudio_v2 : MonoBehaviour {

	private AudioSource audioSrc;

	[SerializeField]
	[Tooltip("Played when the Timer ticks")]
	private AudioClip tickClip;

	[SerializeField]
	[Tooltip("Played when the Timer runs out of time")]
	private AudioClip outOfTimeClip;
	
	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = 0.25f;

		gameObject.GetComponent<Timer_v2>().onTimerTick += PlayTick;
		gameObject.GetComponent<Timer_v2>().onTimerComplete += PlayOutOfTime;
	}

	void OnDestroy()
	{
		gameObject.GetComponent<Timer_v2>().onTimerTick -= PlayTick;
		gameObject.GetComponent<Timer_v2>().onTimerComplete -= PlayOutOfTime;
	}

	void PlayTick()
	{
		audioSrc.PlayOneShot(tickClip);
	}

	void PlayOutOfTime()
	{
		audioSrc.PlayOneShot(outOfTimeClip);
	}
}
