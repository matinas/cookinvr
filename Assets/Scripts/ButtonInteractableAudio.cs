using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(ButtonInteractable))]
[RequireComponent(typeof(AudioSource))]
public class ButtonInteractableAudio : MonoBehaviour {

	[SerializeField]
	private AudioClip clipPress;

	[SerializeField]
	private AudioClip clipRelease;

	private AudioSource audioSrc;

	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		GetComponent<ButtonInteractable>().OnButtonPress += PlayButtonPress;
		GetComponent<ButtonInteractable>().OnButtonRelease += PlayButtonRelease;
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
