﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RecipeController_v1_2 : MonoBehaviour {

	public event Action OnRecipeComplete = () => {};

	public Transform particlesPrefab;
	public Transform spawnPoint;

	public Transform recipe;

	public OMController_v1_2 OMController;

	public static bool isCompleted { get ; private set; }

	private Transform currPlaceholder;
	private int currPHIndex;

	private Transform refRecipe; // Reference recipe

	void Awake()
	{
		OMController.onOMPowerOn += StartRecipe;
		OMController.onOMPowerOff += ResetRecipe;
		OMController.onOMDispatchSuccess += ResetRecipeAndSuccess;

		currPHIndex = 0;
		isCompleted = false;
	}

	void HandleCorrectIngredient(GameObject obj)
	{
		Debug.Log("The Recipe Controller is going to handle a correctly placed ingredient");

		obj.transform.position = currPlaceholder.position;
		obj.transform.rotation = currPlaceholder.rotation;
		obj.transform.parent = recipe.transform;

		CleanCurrentPlaceholder();
		currPHIndex++;
		if (currPHIndex < refRecipe.childCount)
		{
			SetupNextPlaceholder();
		}
		else
		{
			Debug.Log("Recipe completed!");
			isCompleted = true;
			OnRecipeComplete();
		}
	}

	void HandleWrongIngredient(GameObject obj)
	{
		Debug.Log("The Recipe Controller is going to handle a wrongly placed ingredient");
	}

	void StartRecipe(Recipe_v1_2 r)
	{
		refRecipe = GameObject.Instantiate(r.assembledPrefab,Vector3.zero,Quaternion.identity);
		refRecipe.parent = gameObject.transform;
		refRecipe.position = r.assembledPrefab.position;
		refRecipe.rotation = r.assembledPrefab.rotation;

		SetupNextPlaceholder();
	}

	void Reset()
	{
		currPHIndex = 0;
		GameObject.Destroy(refRecipe.gameObject);
		
		for (int c=0; c<recipe.childCount; c++)
			GameObject.Destroy(recipe.GetChild(c).gameObject);

		refRecipe = null;
		currPlaceholder = null;
		isCompleted = false;
	}

	void ResetRecipe(Recipe_v1_2 r)
	{
		Reset();
	}

	void ResetRecipeAndSuccess()
	{
		Reset();

		var particles = GameObject.Instantiate(particlesPrefab, spawnPoint.position, spawnPoint.rotation);
		Destroy(particles.gameObject,2.0f);
	}

	void CleanCurrentPlaceholder()
	{
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnPlaceCorrect -= HandleCorrectIngredient;
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnPlaceWrong -= HandleWrongIngredient;
		currPlaceholder.gameObject.SetActive(false);
	}

	void SetupNextPlaceholder()
	{
		currPlaceholder = refRecipe.GetChild(currPHIndex);
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnPlaceCorrect += HandleCorrectIngredient;
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnPlaceWrong += HandleWrongIngredient;
		currPlaceholder.gameObject.SetActive(true);
	}
}