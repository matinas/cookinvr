using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Order Machine Controller

public class OMController_v1_2 : MonoBehaviour {

	public delegate void OnOMPowerOn(Recipe_v1_2 r);
	public delegate void OnOMPowerOff(Recipe_v1_2 r);
	public delegate void OnOMDispatchError();
	public delegate void OnOMDispatchSuccess();

	[HideInInspector]
	public event OnOMPowerOn onOMPowerOn;
	[HideInInspector]
	public event OnOMPowerOff onOMPowerOff;

	[HideInInspector]
	public event OnOMDispatchError onOMDispatchError;
	[HideInInspector]
	public event OnOMDispatchSuccess onOMDispatchSuccess;

	private bool poweredOn = false;

	[SerializeField]
	private GameObject LED;

	public Material LEDOn;
	public Material LEDOff;

	void Awake()
	{
		OrderSlot_v1_2 OrderSlot = GetComponentInChildren<OrderSlot_v1_2>();

		if (OrderSlot != null)
		{
			OrderSlot.onOrderInserted += HandleOrderInserted;
			OrderSlot.onOrderRemoved += HandleOrderRemoved;
		}
		else
			Debug.Log("No Order Slot associated with the Order Visualizer");

		GetComponentInChildren<ButtonInteractable_v1_2>().OnButtonRelease += HandleButtonClick;
	}

	void OnDestroy()
	{
		OrderSlot_v1_2 OrderSlot = GetComponentInChildren<OrderSlot_v1_2>();

		if (OrderSlot != null)
		{
			OrderSlot.onOrderInserted -= HandleOrderInserted;
			OrderSlot.onOrderRemoved -= HandleOrderRemoved;
		}
		else
			Debug.Log("No Order Slot associated with the Order Visualizer");
	}

	// Use this for initialization
	void Start ()
	{
		poweredOn = false;	
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

		Recipe_v1_2 r = GetOrderRecipe();
		onOMPowerOn.Invoke(r);
	}

	void HandleOrderRemoved()
	{
		if (poweredOn)
			PowerOff();
	}

	void PowerOff()
	{
		Recipe_v1_2 r = GetOrderRecipe();
		onOMPowerOff.Invoke(r);

		ResetMachine();
	}

	void HandleButtonClick()
	{
		if (RecipeController_v1_2.isCompleted)
		{
			onOMDispatchSuccess.Invoke();
			Recipe_v1_2 r = GetOrderRecipe();
			int score = RecipeManager_v1_2.instance.GetRecipeScore(null,r);
			Debug.Log("Well done! Your " + r.name + " gave you " + score + " points");

			ResetMachine();
		}
		else
			onOMDispatchError.Invoke();
	}

	void ResetMachine()
	{
		LED.gameObject.GetComponent<MeshRenderer>().material = LEDOff;
		poweredOn = false;
	}

	Recipe_v1_2 GetOrderRecipe()
	{
		OrderSlot_v1_2 slot = GetComponentInChildren<OrderSlot_v1_2>();
		Order_v1_2 order = slot.o;
		Recipe_v1_2 r = order.recipe.GetComponent<Recipe_v1_2>();

		return r;
	}
}
