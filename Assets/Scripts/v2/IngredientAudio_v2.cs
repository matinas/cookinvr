using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Ingredient_v2))]
public class IngredientAudio_v2 : MonoBehaviour {

	[SerializeField]
	[Tooltip("Played when a new ingredient is pgrabbed")]
	private AudioClip grabClip;

	private AudioSource audioSrc;
	
	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = 0.25f;

		GetComponent<Interactable>().onAttachedToHand += PlayGrabClip;
	}

	void OnDestroy()
	{
		GetComponent<Interactable>().onAttachedToHand -= PlayGrabClip;
	}

	void PlayGrabClip(Hand h)
	{
		audioSrc.PlayOneShot(grabClip);
	}
}
