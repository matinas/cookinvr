using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager_v2 : MonoBehaviour {

	// NOTE: the recipe database is created in compilation time right now, included as part of a non-rendered game object in the hierarchy
	// Anyway, this could have been also loaded in runtime from an XML or something. In this case we could use Resource.Load("Prefabs/prefab_name")
	// to load the different needed ingredient's prefabs and stuff

	public delegate void OnScoreReady(List<int> pscores);

	public static event OnScoreReady onScoreReady;

	public Transform recipeDatabase;

	private List<Recipe_v2> recipes = new List<Recipe_v2>();

	public static RecipeManager_v2 instance = null;

	void Awake()
	{
		if (instance == null)		 // If instance do not exist, set instance to this
			instance = this; 
		else
			if (instance != this)    // If instance already exists and it's not this...
				Destroy(gameObject); // Destroy this. This enforces the Singleton pattern (there can only ever be one instance)

		// Sets this object to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

		Recipe_v2[] childRecipes = recipeDatabase.gameObject.GetComponentsInChildren<Recipe_v2>();
		Debug.Log("There are " + childRecipes.Length + " recipes");
		foreach(Recipe_v2 r in childRecipes)
		{
			Debug.Log(r.ToString());
			recipes.Add(r);
		}
	}

	public Recipe_v2 GetRandomRecipe()
	{
		int index = Random.Range(0,recipes.Count);

		return recipes[index];
	}
}
