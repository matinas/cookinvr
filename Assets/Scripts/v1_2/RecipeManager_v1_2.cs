﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager_v1_2 : MonoBehaviour {

	// NOTE: the recipe database is created in compilation time right now, included as part of a non-rendered game object in the hierarchy
	// Anyway, this could have been also loaded in runtime from an XML or something. In this case we could use Resource.Load("Prefabs/prefab_name")
	// to load the different needed ingredient's prefabs and stuff

	public Transform recipeDatabase;

	private List<Recipe_v1_2> recipes = new List<Recipe_v1_2>();

	public static RecipeManager_v1_2 instance = null;

	void Awake()
	{
		if (instance == null)		 // If instance do not exist, set instance to this
			instance = this; 
		else
			if (instance != this)    // If instance already exists and it's not this...
				Destroy(gameObject); // Destroy this. This enforces the Singleton pattern (there can only ever be one instance).

		// Sets this object to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

		Recipe_v1_2[] childRecipes = recipeDatabase.gameObject.GetComponentsInChildren<Recipe_v1_2>();
		Debug.Log("There are " + childRecipes.Length + " recipes");
		foreach(Recipe_v1_2 r in childRecipes)
		{
			Debug.Log(r.AsString());
			recipes.Add(r);
		}
	}

	public Recipe_v1_2 GetRandomRecipe()
	{
		int index = Random.Range(0,recipes.Count);

		return recipes[index];

		// if (recipes.Count > 0)
		// {
		// 	Recipe_v1_2 r = recipes[index];
		// 	recipes.Remove(r);

		// 	return r;
		// }
		// else
		// 	return null;
	}

	public int GetRecipeScore(List<string> ings, Recipe_v1_2 r)
	{
		// TODO!

		return Random.Range(0,100);
	}
}
