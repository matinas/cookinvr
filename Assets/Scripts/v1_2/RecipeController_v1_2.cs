using System;
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

	public Material plhNormalMat, plhErrorMat;

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

	void HandleCorrectIngredientPlaced(GameObject ing)
	{
		Debug.Log("The Recipe Controller is going to handle a correctly placed ingredient");

		ing.transform.position = currPlaceholder.position;
		ing.transform.rotation = currPlaceholder.rotation;
		ing.transform.parent = recipe.transform;

		LockObject(ing); // Lock the object in place (disable grabbing it) as it is already placed correctly

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

	void HandleWrongIngredientPlaced(GameObject obj)
	{
		Debug.Log("The Recipe Controller is going to handle a wrongly placed ingredient");

		foreach (MeshRenderer mr in currPlaceholder.GetComponentsInChildren<MeshRenderer>())
			mr.material = plhErrorMat;
	}

	void HandleWrongIngredientRemoved(GameObject obj)
	{
		Debug.Log("The Recipe Controller is going to handle a removed wrong ingredient");

		foreach (MeshRenderer mr in currPlaceholder.GetComponentsInChildren<MeshRenderer>())
			mr.material = plhNormalMat;
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
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnPlaceCorrect -= HandleCorrectIngredientPlaced;
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnPlaceWrong -= HandleWrongIngredientPlaced;
		currPlaceholder.gameObject.SetActive(false);
	}

	void SetupNextPlaceholder()
	{
		currPlaceholder = refRecipe.GetChild(currPHIndex);
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnPlaceCorrect += HandleCorrectIngredientPlaced;
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnPlaceWrong += HandleWrongIngredientPlaced;
		currPlaceholder.GetComponent<IngredientPlaceholder_v1_2>().OnRemoveWrong += HandleWrongIngredientRemoved;
		currPlaceholder.gameObject.SetActive(true);
	}

	void LockObject(GameObject ing)
	{
		Destroy(ing.GetComponent<Throwable>());
		Destroy(ing.GetComponent<Interactable>());
		Destroy(ing.GetComponent<Rigidbody>());

		// var throwable = ing.GetComponentInChildren<Throwable>();
		// if (throwable != null)
		// 	Destroy(throwable);
	}
}