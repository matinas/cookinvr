using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager_v2 : MonoBehaviour {

	public List<AudioClip> backgroundMusic;

	public ButtonInteractable_v2 radioButton;

	public static MusicManager_v2 instance = null;

	private AudioSource audioSrc;

	private bool isPlaying;

	void Awake()
	{
		if (instance == null)			// If instance do not exist, set instance to this
			instance = this;
		else
			if (instance != this)		// If instance already exists and it's not this...
				Destroy(gameObject);	// Destroy this. This enforces the Singleton pattern (there can only ever be one instance)

		// Sets this object to not be destroyed when reloading scene
        // DontDestroyOnLoad(gameObject);

		audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = 0.05f;
		audioSrc.Play();

		radioButton.OnButtonRelease += HandleButtonClick;

		isPlaying = true;
	}

	void Update()
	{
		if (audioSrc.time >= (audioSrc.clip.length-0.5f))
			SelectAndPlay();
	}

	void OnDestroy()
	{
		radioButton.OnButtonRelease -= HandleButtonClick;
	}

	void HandleButtonClick()
	{
		isPlaying = !isPlaying;

		if (isPlaying)
			SelectAndPlay();
		else
			audioSrc.Stop();
	}

	void SelectAndPlay()
	{
		audioSrc.clip = SelectRandomTrack();
		audioSrc.Play();
	}

	AudioClip SelectRandomTrack()
	{
		int rand = Random.Range(0,backgroundMusic.Count);

		return backgroundMusic[rand];
	}

}
