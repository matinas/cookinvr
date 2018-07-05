using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager_v2 : MonoBehaviour {

	public List<AudioClip> backgroundMusic;

	public static MusicManager_v2 instance = null;

	private AudioSource audioSrc;

	void Awake()
	{
		if (instance == null)			// If instance do not exist, set instance to this
			instance = this;
		else
			if (instance != this)		// If instance already exists and it's not this...
				Destroy(gameObject);	// Destroy this. This enforces the Singleton pattern (there can only ever be one instance)

		// Sets this object to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

		audioSrc = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (!audioSrc.isPlaying)
		{
			audioSrc.clip = SelectRandomTrack();
			audioSrc.volume = 0.05f;
			audioSrc.Play();
		}
	}

	AudioClip SelectRandomTrack()
	{
		int rand = Random.Range(0,backgroundMusic.Count-1);

		return backgroundMusic[rand];
	}

}
