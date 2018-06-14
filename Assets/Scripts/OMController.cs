using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMController : MonoBehaviour {

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
		OrderSlot orderSlot = GetComponentInChildren<OrderSlot>();

		if (orderSlot != null)
		{
			orderSlot.onOrderInserted += HandleOrderInserted;
			orderSlot.onOrderRemoved += HandleOrderRemoved;
		}
		else
			Debug.Log("No Order Slot associated with the Order Visualizer");

		GetComponentInChildren<ButtonInteractable>().OnButtonRelease += HandleButtonClick;
	}

	void Destroy()
	{
		OrderSlot orderSlot = GetComponentInChildren<OrderSlot>();

		if (orderSlot != null)
		{
			orderSlot.onOrderInserted -= HandleOrderInserted;
			orderSlot.onOrderRemoved -= HandleOrderRemoved;
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
		if (RecipeController.isCompleted)
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
