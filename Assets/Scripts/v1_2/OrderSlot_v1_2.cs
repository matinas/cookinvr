﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSlot_v1_2 : MonoBehaviour {

	public delegate void OnOrderInserted();
	public delegate void OnOrderRemoved();

	private bool isBeingUsed = false;

	public Order_v1_2 o { get; private set; }

	void Start()
	{
		isBeingUsed = false;
	}

	[HideInInspector]
	public event OnOrderInserted onOrderInserted;
	[HideInInspector]
	public event OnOrderRemoved onOrderRemoved;

	void Awake()
	{
		GetComponentInParent<OMController_v1_2>().onOMDispatchSuccess += RemoveOrder;
	}

	void HandleOrderDrop()
	{
		o.transform.position = gameObject.transform.position;
		o.transform.rotation = gameObject.transform.rotation;
		
		onOrderInserted.Invoke();
	}

	void OnTriggerEnter(Collider col)
	{
		if (!isBeingUsed)
		{
			o = col.gameObject.GetComponent<Order_v1_2>();
			if (o != null)
			{
				isBeingUsed = true;
				o.onOrderDropped += HandleOrderDrop;
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (isBeingUsed)
		{
			Order_v1_2 order = col.gameObject.GetComponent<Order_v1_2>();
			if (order == o)
			{
				isBeingUsed = false;
				o.onOrderDropped -= HandleOrderDrop;
				onOrderRemoved.Invoke();
				o = null;
			}
		}
	}

	void RemoveOrder()
	{
		if (o != null)
		{
			GameObject.Destroy(o.gameObject);
			isBeingUsed = false;
		}
	}
}
