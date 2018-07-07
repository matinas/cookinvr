using System;
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

	private GameObject currentTopping;

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
		DebugIngredients = currentIngredients;

		for (int c=currentRecipe.childCount-1; c>=0; c--)
		{
			Ingredient_v2 ingredient = currentRecipe.GetChild(c).GetComponent<Ingredient_v2>();
			if (ingredient != null)
				ingredient.DispatchRestore();
			else
				Debug.Log("The recipe contains a non-ingredient object");
		}

		if (currentTopping != null)
		{
			Destroy(currentTopping);
		}
	}

	void HandleIngredientPlaced(Hand h)
	{
		if (currentIngredient != null)
		{
			Debug.Log(currentIngredient.ingredientName + " placed! ");

			currentIngredients.Add(currentIngredient.ingredientName);
			currentIngredient.transform.parent = currentRecipe;
			//currentIngredient.GetComponent<Rigidbody>().isKinematic = true;

			DebugIngredients = currentIngredients; // FIXME: Just for Debug purposes, remove later!

			if (onIngredientPlaced != null)
				onIngredientPlaced.Invoke();			
		}
	}

	void HandleIngredientRemoved(Hand h)
	{
		if (currentIngredients.Remove(currentIngredient.ingredientName))
		{
			Debug.Log(currentIngredient.ingredientName + " removed! ");
			DebugIngredients = currentIngredients; // FIXME: Just for Debug purposes, remove later!
		}
		else
			Debug.Log("Couldn't remove " + currentIngredient.ingredientName);
	}

	void HandleToppingPlaced(Hand h)
	{
		if (currentTopping != null)
		{
			Destroy(currentTopping.GetComponent<Throwable>());
			Destroy(currentTopping.GetComponent<VelocityEstimator>());
			Destroy(currentTopping.GetComponent<Interactable>());
			Destroy(currentTopping.GetComponent<Rigidbody>());

			currentTopping.transform.position = currentIngredient.gameObject.transform.position;
			currentTopping.transform.parent = currentIngredient.transform;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		Ingredient_v2 tmpIngredient = col.GetComponent<Ingredient_v2>();

		if (tmpIngredient != null)
		{
			currentIngredient = tmpIngredient;

			if (currentIngredient.GetComponentInParent<Hand>() != null) // Collides and it's attached to a hand, so we wait until it's dropped
			{
				Interactable i = currentIngredient.GetComponent<Interactable>();
				
				i.onDetachedFromHand -= HandleIngredientPlaced; // This was added to solve a strange issue that makes OnTriggerEnter execute twice for some ings
				i.onDetachedFromHand += HandleIngredientPlaced;
			}
			else // Collides and it's not attached to a hand, so it's flying via physics or something...
				HandleIngredientPlaced(null);
		}
		else
		{
			if (col.gameObject.layer == LayerMask.NameToLayer("Topping"))
			{
				Interactable i = col.GetComponent<Interactable>();
				currentTopping = col.gameObject;

				i.onDetachedFromHand -= HandleToppingPlaced; // This was added to solve a strange issue that makes OnTriggerEnter execute twice for some ings
				i.onDetachedFromHand += HandleToppingPlaced;
			}
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
		else
		{
			if (col.gameObject.layer == LayerMask.NameToLayer("Topping"))
			{
				Interactable i = col.GetComponent<Interactable>();
				i.onDetachedFromHand -= HandleToppingPlaced;
				currentTopping = null;
			}
		}
	}

	public bool IsEmpty()
	{
		return (currentIngredients.Count == 0);
	}
}
