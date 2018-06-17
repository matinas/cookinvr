using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(ButtonInteractable_v2))]
[RequireComponent(typeof(AudioSource))]
public class ButtonInteractableAudio_v2 : MonoBehaviour {

	[SerializeField]
	private AudioClip clipPress;

	[SerializeField]
	private AudioClip clipRelease;

	private AudioSource audioSrc;

	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		GetComponent<ButtonInteractable_v2>().OnButtonPress += PlayButtonPress;
		GetComponent<ButtonInteractable_v2>().OnButtonRelease += PlayButtonRelease;
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
