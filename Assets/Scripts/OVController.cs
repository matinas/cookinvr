using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVController : MonoBehaviour {

	public delegate void OnOVPowerOn();
	public delegate void OnOVPowerOff();

	[HideInInspector]
	public event OnOVPowerOn onOVPowerOn;
	[HideInInspector]
	public event OnOVPowerOff onOVPowerOff;

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

		onOVPowerOn.Invoke();
	}

	void HandleOrderRemoved()
	{
		if (poweredOn)
			PowerOff();
	}

	void PowerOff()
	{
		LED.gameObject.GetComponent<MeshRenderer>().material = LEDOff;
		poweredOn = false;

		onOVPowerOff.Invoke();
	}
}
