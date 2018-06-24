using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(ButtonInteractable_v1_2))]
[RequireComponent(typeof(AudioSource))]
public class ButtonInteractableAudio_v1_2 : MonoBehaviour {

	[SerializeField]
	private AudioClip clipPress;

	[SerializeField]
	private AudioClip clipRelease;

	private AudioSource audioSrc;

	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		GetComponent<ButtonInteractable_v1_2>().OnButtonPress += PlayButtonPress;
		GetComponent<ButtonInteractable_v1_2>().OnButtonRelease += PlayButtonRelease;
	}

	void PlayButtonPress()
	{
		audioSrc.PlayOneShot(clipPress);
	}

	void PlayButtonRelease()
	{
		audioSrc.PlayOneShot(clipRelease);
	}

}
