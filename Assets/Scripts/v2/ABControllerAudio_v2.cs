using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ABController_v2))]
public class ABControllerAudio_v2 : MonoBehaviour {

	[SerializeField]
	[Tooltip("Played when a new ingredient is placed on the assembly board")]
	private AudioClip squishClip;

	private AudioSource audioSrc;
	
	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = 0.25f;

		GetComponent<ABController_v2>().onIngredientPlaced += PlaySquishClip;
	}

	void OnDestroy()
	{
		GetComponent<ABController_v2>().onIngredientPlaced -= PlaySquishClip;
	}

	void PlaySquishClip()
	{
		audioSrc.PlayOneShot(squishClip);
	}
}
