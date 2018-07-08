using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ABControllerAudio_v2 : MonoBehaviour {

	[SerializeField]
	[Tooltip("Played when a new ingredient is placed on the assembly board")]
	private AudioClip squishClip;

	private AudioSource audioSrc;
	
	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = 0.25f;

		GetComponentInParent<ABController_v2>().onIngredientPlaced += PlaySquishClip;
	}

	void OnDestroy()
	{
		ABController_v2 ABController = GetComponentInParent<ABController_v2>();
		
		if (ABController != null)
			ABController.onIngredientPlaced -= PlaySquishClip;
	}

	void PlaySquishClip()
	{
		audioSrc.PlayOneShot(squishClip);
	}
}
