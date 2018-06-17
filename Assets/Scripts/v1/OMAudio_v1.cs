using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(OMController_v1))]
public class OMAudio_v1 : MonoBehaviour {

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
		GetComponent<OMController_v1>().onOMPowerOn += PlayPowerOn;
		GetComponent<OMController_v1>().onOMPowerOff += PlayPowerOff;
		GetComponent<OMController_v1>().onOMDispatchSuccess += PlayDispatchSuccess;
		GetComponent<OMController_v1>().onOMDispatchError += PlayDispatchError;
	}

	void PlayPowerOn()
	{
		audioSrc.PlayOneShot(powerOnClip);
	}

	void PlayPowerOff()
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
