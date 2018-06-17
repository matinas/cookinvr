using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Ingredient_v2 : MonoBehaviour {

    public string ingredientName;

    // TODO: add state varialbe to state whether the ingredient is boiled, cooked, fried, chopped, etc

	void Awake()
	{
        if (ingredientName == null)
            ingredientName = "Unknown";
	}
}