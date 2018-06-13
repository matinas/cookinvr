using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RecipeController : MonoBehaviour {

	public event Action OnRecipeComplete = () => {};

	public Transform recipePrefab;

	public Transform recipe;

	public OVController OVController;

	public bool isCompleted { get ; private set; }

	private Transform currPlaceholder;
	private int currPHIndex;

	private Transform refRecipe; // Reference recipe

	void Awake()
	{
		OVController.onOVPowerOn += StartRecipe;
		OVController.onOVPowerOff += ResetRecipe;

		currPHIndex = 0;
		isCompleted = false;
	}

	void HandleCorrectIngredient(GameObject obj)
	{
		Debug.Log("The Recipe Controller is going to handle a correctly placed ingredient");

		obj.transform.position = currPlaceholder.position;
		obj.transform.rotation = currPlaceholder.rotation;
		obj.transform.parent = recipe.transform;

		//Destroy(obj.GetComponent<Throwable>());
		//Destroy(obj.GetComponent<Rigidbody>());

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

	void StartRecipe()
	{
		refRecipe = GameObject.Instantiate(recipePrefab,Vector3.zero,Quaternion.identity);
		refRecipe.parent = gameObject.transform;

		SetupNextPlaceholder();
	}

	void ResetRecipe()
	{
		currPHIndex = 0;
		GameObject.Destroy(refRecipe.gameObject);
		
		for (int c=0; c<recipe.childCount; c++)
			GameObject.Destroy(recipe.GetChild(c).gameObject);

		refRecipe = null;
		currPlaceholder = null;
	}

	void CleanCurrentPlaceholder()
	{
		currPlaceholder.GetComponent<IngredientPlaceholder>().OnPlaceCorrect -= HandleCorrectIngredient;
		currPlaceholder.GetComponent<IngredientPlaceholder>().OnPlaceWrong -= HandleWrongIngredient;
		currPlaceholder.gameObject.SetActive(false);
	}

	void SetupNextPlaceholder()
	{
		currPlaceholder = refRecipe.GetChild(currPHIndex);
		currPlaceholder.GetComponent<IngredientPlaceholder>().OnPlaceCorrect += HandleCorrectIngredient;
		currPlaceholder.GetComponent<IngredientPlaceholder>().OnPlaceWrong += HandleWrongIngredient;
		currPlaceholder.gameObject.SetActive(true);
	}
}