using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndicatorController_v2 : MonoBehaviour {

	public GameObject indicatorPrefab;

	public Transform BoxPos, OMPos, AssemblyPos;

	public Transform orderSlot;

	private bool orderGrabbed, orderSlotted, ingredientPlaced;

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
		orderGrabbed = false;
		orderSlotted = false;
		ingredientPlaced = false;

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
	}
	
	void HandleOrderReady()
	{
		if (!orderGrabbed)
		{
			orderGrabbed = true;

			tooltip.text = "GRAB\nORDER!";

			indicator.transform.position = BoxPos.position;
			indicator.transform.rotation = BoxPos.rotation;
			indicator.transform.parent = BoxPos.transform;

			indicator.SetActive(true);
		}
	}

	void HandleOrderGrabbed()
	{
		if (!orderSlotted)
		{
			orderSlotted = true;

			indicator.transform.position = OMPos.position;
			indicator.transform.rotation = OMPos.rotation;

			tooltip.text = "SLOT\nORDER!";

			indicator.SetActive(true);
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

		Destroy(gameObject);
	}

	void HandleOrderInserted()
	{
		if (!ingredientPlaced)
		{
			ingredientPlaced = true;

			indicator.transform.position = AssemblyPos.position;
			indicator.transform.rotation = AssemblyPos.rotation;

			tooltip.text = "PLACE\nINGREDIENTS";

			indicator.SetActive(true);
		}
	}
}
