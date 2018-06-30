using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndicatorController_v2 : MonoBehaviour {

	public GameObject indicatorPrefab;

	public Transform BoxPos, OMPos, AssemblyPos, DispatchPos;
	public string BoxTooltip, OMTooltip, AssemblyTooltip, DispatchTooltip;

	public Transform orderSlot;
	public Transform ABController;

	private bool grabOrder, slotOrder, placeIngredient, dispatchOrder;

	private GameObject indicator;

	private TextMeshPro tooltip;

	void Awake()
	{
		indicator = GameObject.Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity);
		indicator.SetActive(false);

		tooltip = indicator.GetComponentInChildren<TextMeshPro>();
	}

	// Use this for initialization
	void Start ()
	{
		grabOrder = false;
		slotOrder = false;
		placeIngredient = false;
		dispatchOrder = false;

		// We consider the future case in which there will be multiple orders to select from...
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Order");
		foreach(GameObject go in gos)
		{
			Order_v2 o = go.GetComponent<Order_v2>();
			if (o != null)
			{
				o.onOrderReady += HandleOrderReady;
				o.onOrderGrabbed += HandleOrderGrabbed;
				o.onOrderDropped += Deactivate;
			}
		}

		OrderSlot_v2 os = orderSlot.GetComponent<OrderSlot_v2>();
		os.onOrderInserted += HandleOrderInserted;
		os.onOrderRemoved += Deactivate;
		OMController_v2 omc = orderSlot.GetComponentInParent<OMController_v2>();
		omc.onOMDispatch += Deactivate;
		ABController_v2 abc = ABController.GetComponent<ABController_v2>();
		abc.onIngredientPlaced += HandleIngredientPlaced;

		AddCarriageReturn(ref BoxTooltip);
		AddCarriageReturn(ref OMTooltip);
		AddCarriageReturn(ref AssemblyTooltip);
		AddCarriageReturn(ref DispatchTooltip);
	}
	
	void HandleOrderReady()
	{
		if (!grabOrder)
		{
			grabOrder = true;
			ShowTooltip(BoxPos, BoxTooltip);
		}
	}

	void HandleOrderGrabbed()
	{
		if (!slotOrder)
		{
			slotOrder = true;
			ShowTooltip(OMPos, OMTooltip);
		}
	}

	void HandleOrderInserted()
	{
		if (!placeIngredient)
		{
			placeIngredient = true;
			ShowTooltip(AssemblyPos, AssemblyTooltip);
		}
	}

	void HandleIngredientPlaced()
	{
		if (!dispatchOrder)
		{
			dispatchOrder = true;
			ShowTooltip(DispatchPos, DispatchTooltip);
		}
	}

	void Deactivate()
	{
		if (indicator.active)
			indicator.SetActive(false);
	}

	void Deactivate(int score)
	{
		// After an order is dispatched we can destroy the complete Indication System
		// FIXME: If more indications are added later this have to be changed!

		GameObject[] gos = GameObject.FindGameObjectsWithTag("Order");
		foreach(GameObject go in gos)
		{
			Order_v2 o = go.GetComponent<Order_v2>();
			if (o != null)
			{
				o.onOrderReady -= HandleOrderReady;
				o.onOrderGrabbed -= HandleOrderGrabbed;
				o.onOrderDropped -= Deactivate;
			}
		}

		OrderSlot_v2 os = orderSlot.GetComponent<OrderSlot_v2>();
		os.onOrderInserted -= HandleOrderInserted;
		os.onOrderRemoved -= Deactivate;
		OMController_v2 omc = orderSlot.GetComponentInParent<OMController_v2>();
		omc.onOMDispatch -= Deactivate;
		ABController_v2 abc = ABController.GetComponent<ABController_v2>();
		abc.onIngredientPlaced -= HandleIngredientPlaced;

		Destroy(gameObject);
	}

	void ShowTooltip(Transform t, string text)
	{
		indicator.transform.position = t.position;
		indicator.transform.rotation = t.rotation;
		indicator.transform.parent = t;

		tooltip.text = text;

		indicator.SetActive(true);
	}

	void AddCarriageReturn(ref string s)
	{
		s = s.Replace(" ","\n");
	}
}
