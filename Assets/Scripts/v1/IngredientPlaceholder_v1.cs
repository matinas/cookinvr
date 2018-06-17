using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class IngredientPlaceholder_v1 : MonoBehaviour {

	public event Action<GameObject> OnPlaceCorrect = (obj) => {};
	public event Action<GameObject> OnPlaceWrong = (obj) => {};

	private GameObject currIngredient;

	void HandleCorrectIngredientDrop(Hand h)
	{
		Debug.Log("Correct ingredient placed!");

		OnPlaceCorrect(currIngredient);
	}

	void HandleWrongIngredientDrop(Hand h)
	{
		Debug.Log("Wrong ingredient placed!");

		OnPlaceWrong(currIngredient);
	}

	void OnTriggerEnter(Collider col)
	{
		currIngredient = col.gameObject;

		// FIXME: this comparisons for checking equality between placeholder and ingredient itself should
		// 		  be better implemented by comparing tags instead of game objects names

		if (col.gameObject.name == gameObject.name)
		{
			Debug.Log("Correct ingredient entered placeholder!");

			if (currIngredient.GetComponentInParent<Hand>() != null) // Collides and it's attached to a hand, so we wait until it's dropped
			{
				currIngredient.GetComponent<Interactable>().onDetachedFromHand += HandleCorrectIngredientDrop;
			}
			else // Collides and it's not attached to a hand, so it's flying via physics or something...
			{
				Debug.Log("Correct ingredient dropped!");
				OnPlaceCorrect(currIngredient);
			}
		}
		else
		{
			Debug.Log("Wrong ingredient entered placeholder!");
			currIngredient.GetComponent<Interactable>().onDetachedFromHand += HandleWrongIngredientDrop;
		}
	}

    void OnTriggerExit(Collider col)
	{
		// FIXME: this comparisons for checking equality between placeholder and ingredient itself should
		// 		  be better implemented by comparing tags instead of game objects names

		if (col.gameObject.name == gameObject.name)
		{
			Debug.Log("Correct ingredient exited placeholder!");
			currIngredient.GetComponent<Interactable>().onDetachedFromHand -= HandleCorrectIngredientDrop;
		}
		else
		{
			Debug.Log("Wrong ingredient exited placeholder!");
			currIngredient.GetComponent<Interactable>().onDetachedFromHand -= HandleWrongIngredientDrop;
		}
	}
}
