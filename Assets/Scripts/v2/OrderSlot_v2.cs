using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSlot_v2 : MonoBehaviour {

	public delegate void OnOrderInserted();
	public delegate void OnOrderRemoved();

	private bool isBeingUsed = false;

	public Order_v2 order { get; private set; }

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
		GetComponentInParent<OMController_v2>().onOMDispatch += RemoveOrder;
	}

	void HandleOrderDrop()
	{
		order.transform.position = gameObject.transform.position;
		order.transform.rotation = gameObject.transform.rotation;
		
		onOrderInserted.Invoke();
	}

	void OnTriggerEnter(Collider col)
	{
		if (!isBeingUsed)
		{
			order = col.gameObject.GetComponent<Order_v2>();
			if (order != null)
			{
				isBeingUsed = true;
				order.onOrderDropped += HandleOrderDrop;
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (isBeingUsed)
		{
			Order_v2 tmp_order = col.gameObject.GetComponent<Order_v2>();
			if (tmp_order == order)
			{
				isBeingUsed = false;
				order.onOrderDropped -= HandleOrderDrop;
				onOrderRemoved.Invoke();
				order = null;
			}
		}
	}

	void RemoveOrder(int score)
	{
		if (order != null)
		{
			GameObject.Destroy(order.gameObject);
			isBeingUsed = false;
		}
	}
}
