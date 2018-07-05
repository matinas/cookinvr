using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SmartIngredientSpawner_v2))]
public class SmartIngredientSpawnerAudio_v2 : MonoBehaviour {

	[SerializeField]
	[Tooltip("Played when the Spawner spawns a new ingredient")]
	private AudioClip spawnClip;  
	
	private AudioSource audioSrc;

	void Awake()
	{
		audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = 0.1f;

		GetComponent<SmartIngredientSpawner_v2>().onSpawn += PlaySpawnClip;
	}

	void OnDestroy()
	{
		GetComponent<SmartIngredientSpawner_v2>().onSpawn -= PlaySpawnClip;
	}

	void PlaySpawnClip()
	{
		audioSrc.PlayOneShot(spawnClip);
	}
}
