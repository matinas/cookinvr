using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMDisplay_v2 : MonoBehaviour {

	[SerializeField]
	[Tooltip("Reference to the Display game object")]
	private Transform display;

	[SerializeField]
	[Tooltip("Position in which the disassembled recipe will be placed")]
	private Transform recipePos;
	
	// Use this for initialization
	void Awake()
	{
		display.gameObject.SetActive(false);

		GetComponent<OMController_v2>().onOMPowerOn += TurnOnDisplay;
		GetComponent<OMController_v2>().onOMPowerOff += TurnOffDisplay;
		GetComponent<OMController_v2>().onOMDispatch += TurnOffDisplay;
	}
	
	void TurnOnDisplay(Recipe_v2 r)
	{
		display.gameObject.SetActive(true);

		// FIXME: the prefab instancing each time the order machine powers on can be avoided for performance reasons by caching
		//		  the last recipe shown, so to check whether the new recipe is the same as the current one, and if so use the already instanced prefab

		Transform disRecipe = GameObject.Instantiate(r.disassembledPrefab,recipePos.position,recipePos.rotation);
		disRecipe.parent = recipePos;
	}

	void TurnOffDisplay(Recipe_v2 r)
	{
		ClearDisplay();
	}

	void TurnOffDisplay(int score)
	{
		ClearDisplay();
	}

	void ClearDisplay()
	{
		for (int c=0; c<recipePos.childCount; c++)
			GameObject.Destroy(recipePos.GetChild(c).gameObject);

		display.gameObject.SetActive(false);
	}
}
