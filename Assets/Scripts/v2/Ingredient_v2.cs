using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Ingredient_v2 : MonoBehaviour {

    public event Action<Ingredient_v2> OnDispatch = (ing) => {};
    public event Action<Ingredient_v2> OnOutOfRange = (ing) => {};

    public string ingredientName;

    public AudioClip grab;

    private AudioSource audioSrc;

    // TODO: add state variable to state whether the ingredient is boiled, cooked, fried, chopped, etc

	void Awake()
	{
        if (ingredientName == null)
            ingredientName = "Unknown";

        audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = 0.25f;
	}

    public void DispatchRestore()
    {
        Debug.Log("Ingredient will be restored after dispatch");

        OnDispatch(this);
    }

    void OnAttachedToHand()
    {
        audioSrc.clip = grab;
        audioSrc.Play();
    }

    public void OutOfRangeRestore()
    {
        Debug.Log("Ingredient will be restored after getting out of range");

        OnOutOfRange(this);
    }
}