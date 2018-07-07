using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Order Machine Controller

public class OMController_v2 : MonoBehaviour {

	public delegate void OnOMPowerOn(Recipe_v2 r);
	public delegate void OnOMPowerOff(Recipe_v2 r);
	public delegate void OnOMDispatch(int score);
	public delegate void OnOMDispatchError();

	[HideInInspector]
	public event OnOMPowerOn onOMPowerOn;
	[HideInInspector]
	public event OnOMPowerOff onOMPowerOff;
	[HideInInspector]
	public event OnOMDispatch onOMDispatch;
	public event OnOMDispatchError onOMDispatchError;

	public Material LEDOn;
	public Material LEDOff;

	[SerializeField]
	[Tooltip("Reference to the Order Machine's LED child object")]
	private Transform LED;

	[SerializeField]
	[Tooltip("Reference to the Assembly Board Controller")]
	private ABController_v2 ABController;

	private bool poweredOn = false;

	void Awake()
	{
		OrderSlot_v2 OrderSlot_v2 = GetComponentInChildren<OrderSlot_v2>();

		if (OrderSlot_v2 != null)
		{
			OrderSlot_v2.onOrderInserted += HandleOrderInserted;
			OrderSlot_v2.onOrderRemoved += HandleOrderRemoved;
		}
		else
			Debug.Log("No Order Slot associated with the Order Visualizer");

		GetComponentInChildren<ButtonInteractable_v2>().OnButtonRelease += HandleButtonClick;

		poweredOn = false;
	}

	void Destroy()
	{
		OrderSlot_v2 OrderSlot_v2 = GetComponentInChildren<OrderSlot_v2>();

		if (OrderSlot_v2 != null)
		{
			OrderSlot_v2.onOrderInserted -= HandleOrderInserted;
			OrderSlot_v2.onOrderRemoved -= HandleOrderRemoved;
		}
		else
			Debug.Log("No Order Slot associated with the Order Visualizer");
	}

	void HandleOrderInserted()
	{
		if (!poweredOn)
			PowerOn();
	}

	void PowerOn()
	{
		LED.gameObject.GetComponent<MeshRenderer>().material = LEDOn;
		poweredOn = true;

		Recipe_v2 r = GetOrderRecipe();
		onOMPowerOn.Invoke(r);
	}

	void HandleOrderRemoved()
	{
		if (poweredOn)
			PowerOff();
	}

	void PowerOff()
	{
		Recipe_v2 r = GetOrderRecipe();
		onOMPowerOff.Invoke(r);

		ResetMachine();
	}

	void HandleButtonClick()
	{
		if (poweredOn && !ABController.IsEmpty())
		{
			ScoreData score = EvaluateRecipe();
			onOMDispatch.Invoke(score.finalScore);

			ResetMachine();
		}
		else
			onOMDispatchError.Invoke();
	}

	ScoreData EvaluateRecipe()
	{
		List<string> recipeIngs = new List<string>(ABController.currentIngredients);
		Recipe_v2 r = GetOrderRecipe();

		Debug.Log("Recipe placed ingrs: [" + string.Join( ",", recipeIngs.ToArray()) + "]");
		Debug.Log("Recipe correct ings [" + string.Join( ",", r.ingredients.ToArray()) + "]");

		ScoreData score = ScoreManager_v2.instance.ProcessScore(recipeIngs, r);

		Debug.Log("Well done! Your " + r.name + " gave you " + score.finalScore + " points");

		return score;
	}

	void ResetMachine()
	{
		LED.gameObject.GetComponent<MeshRenderer>().material = LEDOff;
		poweredOn = false;
	}

	Recipe_v2 GetOrderRecipe()
	{
		OrderSlot_v2 slot = GetComponentInChildren<OrderSlot_v2>();
		Order_v2 order = slot.order;
		Recipe_v2 r = order.recipe.GetComponent<Recipe_v2>();

		return r;
	}
}
