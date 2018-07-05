using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(OMController_v2))]
public class OMAudio_v2 : MonoBehaviour {

	private AudioSource audioSrc;

	[SerializeField]
	[Tooltip("Played when the Order Visualizer is powered on")]
	private AudioClip powerOnClip;  
	
	[SerializeField]
	[Tooltip("Played when the Order Visualizer is powered off")]
	private AudioClip powerOffClip;

	[SerializeField]
	[Tooltip("Played when the Order Visualizer dispatchs an order successfully")]
	private AudioClip dispatchSuccess;

	[SerializeField]
	[Tooltip("Played when the Order Visualizer dispatchs an order but gets an error")]
	private AudioClip dispatchError;

	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = 0.5f;

		GetComponent<OMController_v2>().onOMPowerOn += PlayPowerOn;
		GetComponent<OMController_v2>().onOMPowerOff += PlayPowerOff;
		GetComponent<OMController_v2>().onOMDispatchError += PlayDispatchError;
		// GetComponent<OMController_v2>().onOMDispatch += PlayDispatch;
	}

	void OnDestroy()
	{
		GetComponent<OMController_v2>().onOMPowerOn -= PlayPowerOn;
		GetComponent<OMController_v2>().onOMPowerOff -= PlayPowerOff;
		GetComponent<OMController_v2>().onOMDispatchError -= PlayDispatchError;
		// GetComponent<OMController_v2>().onOMDispatch -= PlayDispatch;
	}

	void PlayPowerOn(Recipe_v2 r)
	{
		audioSrc.PlayOneShot(powerOnClip);
	}

	void PlayPowerOff(Recipe_v2 r)
	{
		audioSrc.PlayOneShot(powerOffClip);
	}

	void PlayDispatch(int score)
	{
		audioSrc.PlayOneShot(dispatchSuccess);
	}

	void PlayDispatchError()
	{
		audioSrc.PlayOneShot(dispatchError);
	}
}
