using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSlot : MonoBehaviour {

	public delegate void OnOrderInserted();
	public delegate void OnOrderRemoved();

	private bool isBeingUsed = false;

	private Order o = null;

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
		GetComponentInParent<OMController>().onOMDispatchSuccess += RemoveOrder;
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
			o = col.gameObject.GetComponent<Order>();
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
			Order order = col.gameObject.GetComponent<Order>();
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
		GameObject.Destroy(o.gameObject);
		isBeingUsed = false;
	}
}
