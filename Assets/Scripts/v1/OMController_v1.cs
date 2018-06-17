using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Order Machine Controller

public class OMController_v1 : MonoBehaviour {

	public delegate void OnOMPowerOn();
	public delegate void OnOMPowerOff();
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
		OrderSlot_v1 OrderSlot = GetComponentInChildren<OrderSlot_v1>();

		if (OrderSlot != null)
		{
			OrderSlot.onOrderInserted += HandleOrderInserted;
			OrderSlot.onOrderRemoved += HandleOrderRemoved;
		}
		else
			Debug.Log("No Order Slot associated with the Order Visualizer");

		GetComponentInChildren<ButtonInteractable_v1>().OnButtonRelease += HandleButtonClick;
	}

	void Destroy()
	{
		OrderSlot_v1 OrderSlot = GetComponentInChildren<OrderSlot_v1>();

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

		onOMPowerOn.Invoke();
	}

	void HandleOrderRemoved()
	{
		if (poweredOn)
			PowerOff();
	}

	void PowerOff()
	{
		ResetMachine();

		onOMPowerOff.Invoke();
	}

	void HandleButtonClick()
	{
		if (RecipeController_v1.isCompleted)
		{
			onOMDispatchSuccess.Invoke();
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
}
