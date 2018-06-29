using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Ingredient_v2 : MonoBehaviour {

    public event Action<Ingredient_v2> OnDispatch = (ing) => {};
    public event Action<Ingredient_v2> OnOutOfRange = (ing) => {};

    public string ingredientName;

    // TODO: add state variable to state whether the ingredient is boiled, cooked, fried, chopped, etc

	void Awake()
	{
        if (ingredientName == null)
            ingredientName = "Unknown";
	}

    public void DispatchRestore()
    {
        Debug.Log("Ingredient will be restored after dispatch");

        OnDispatch(this);
    }

    public void OutOfRangeRestore()
    {
        Debug.Log("Ingredient will be restored after getting out of range");

        OnOutOfRange(this);
    }
}