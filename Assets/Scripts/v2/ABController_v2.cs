﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Assembly Board Controller (kind of Recipe Controller in v1)

public class ABController_v2 : MonoBehaviour {

	public delegate void OnIngredientPlaced();

	public event OnIngredientPlaced onIngredientPlaced;

	public Transform particlesPrefab;
	public Transform spawnPoint;

	public Transform currentRecipe;

	public List<string> currentIngredients { get; private set; }

	public List<string> DebugIngredients;

	private Ingredient_v2 currentIngredient; // The ingredient that is about to be placed (being grabbed) or has just been placed

	[SerializeField]
	[Tooltip("Reference to the Order Machine Controller")]
	private OMController_v2 OMController_v2;

	void Awake()
	{
		OMController_v2.onOMPowerOn += InitAssembly;
		OMController_v2.onOMPowerOff += InitAssembly;
		OMController_v2.onOMDispatch += DispatchRecipe;

		currentIngredients = new List<string>();
	}

	void Destroy()
	{
		OMController_v2.onOMPowerOn -= InitAssembly;
		OMController_v2.onOMPowerOff -= InitAssembly;
		OMController_v2.onOMDispatch -= DispatchRecipe;
	}

	void InitAssembly(Recipe_v2 r)
	{
		currentIngredient = null;
	}

	void DispatchRecipe(int score)
	{
		var particles = GameObject.Instantiate(particlesPrefab, spawnPoint.position, spawnPoint.rotation);
		Destroy(particles.gameObject,2.0f);

		currentIngredients.Clear();

		for (int c=currentRecipe.childCount-1; c>=0; c--)
		{
			Ingredient_v2 ingredient = currentRecipe.GetChild(c).GetComponent<Ingredient_v2>();
			if (ingredient != null)
				ingredient.DispatchRestore();
			else
				Debug.Log("The recipe contains a non-ingredient object");
		}

		DebugIngredients = currentIngredients;
	}

	void HandleIngredientPlaced(Hand h)
	{
		Debug.Log("Ingredient placed!");

		if (currentIngredient != null)
		{
			currentIngredients.Add(currentIngredient.ingredientName);
			currentIngredient.transform.parent = currentRecipe;
	
			DebugIngredients = currentIngredients; // FIXME: Just for Debug purposes, remove later!

			if (onIngredientPlaced != null)
				onIngredientPlaced.Invoke();
		}
	}

	void HandleIngredientRemoved(Hand h)
	{
		Debug.Log("Ingredient removed!");

		currentIngredients.Remove(currentIngredient.ingredientName);

		DebugIngredients = currentIngredients; // FIXME: Just for Debug purposes, remove later!
	}

	void OnTriggerEnter(Collider col)
	{
		currentIngredient = col.GetComponent<Ingredient_v2>();

		if (currentIngredient != null)
		{
			if (currentIngredient.GetComponentInParent<Hand>() != null) // Collides and it's attached to a hand, so we wait until it's dropped
				currentIngredient.GetComponent<Interactable>().onDetachedFromHand += HandleIngredientPlaced;
			else // Collides and it's not attached to a hand, so it's flying via physics or something...
				HandleIngredientPlaced(null);
		}
	}

	void OnTriggerExit(Collider col)
	{
		currentIngredient = col.GetComponent<Ingredient_v2>();

		if (currentIngredient != null)
		{
			if (currentIngredient.GetComponentInParent<Hand>() != null) // Exits collider and it's attached to a hand, so we remove the handler
				currentIngredient.GetComponent<Interactable>().onDetachedFromHand -= HandleIngredientPlaced;

			HandleIngredientRemoved(null);
		}
	}

	public bool IsEmpty()
	{
		return (currentIngredients.Count == 0);
	}
}
