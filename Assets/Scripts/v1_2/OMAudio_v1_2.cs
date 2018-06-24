using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(OMController_v1_2))]
public class OMAudio_v1_2 : MonoBehaviour {

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
		GetComponent<OMController_v1_2>().onOMPowerOn += PlayPowerOn;
		GetComponent<OMController_v1_2>().onOMPowerOff += PlayPowerOff;
		GetComponent<OMController_v1_2>().onOMDispatchSuccess += PlayDispatchSuccess;
		GetComponent<OMController_v1_2>().onOMDispatchError += PlayDispatchError;
	}

	void OnDestroy()
	{
		GetComponent<OMController_v1_2>().onOMPowerOn -= PlayPowerOn;
		GetComponent<OMController_v1_2>().onOMPowerOff -= PlayPowerOff;
		GetComponent<OMController_v1_2>().onOMDispatchSuccess -= PlayDispatchSuccess;
		GetComponent<OMController_v1_2>().onOMDispatchError -= PlayDispatchError;
	}

	void PlayPowerOn(Recipe_v1_2 r)
	{
		audioSrc.PlayOneShot(powerOnClip);
	}

	void PlayPowerOff(Recipe_v1_2 r)
	{
		audioSrc.PlayOneShot(powerOffClip);
	}

	void PlayDispatchSuccess()
	{
		audioSrc.PlayOneShot(dispatchSuccess);
	}

	void PlayDispatchError()
	{
		audioSrc.PlayOneShot(dispatchError);
	}
}
