using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(OVController))]
public class OVAudio : MonoBehaviour {

	private AudioSource audioSrc;

	[SerializeField]
	[Tooltip("Played when the Order Visulizar is powered on")]
	private AudioClip powerOnClip;  
	
	[SerializeField]
	[Tooltip("Played when the Order Visulizar is powered off")]
	private AudioClip powerOffClip;

	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		GetComponent<OVController>().onOVPowerOn += PlayPowerOn;
		GetComponent<OVController>().onOVPowerOff += PlayPowerOff;
	}

	void PlayPowerOn()
	{
		audioSrc.PlayOneShot(powerOnClip);
	}

	void PlayPowerOff()
	{
		audioSrc.PlayOneShot(powerOffClip);
	}
}
