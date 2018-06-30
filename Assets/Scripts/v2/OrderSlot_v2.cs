using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSlot_v2 : MonoBehaviour {

	public delegate void OnOrderInserted();
	public delegate void OnOrderRemoved();

	private bool isBeingUsed = false;

	public Order_v2 order { get; private set; }

	[HideInInspector]
	public event OnOrderInserted onOrderInserted;
	[HideInInspector]
	public event OnOrderRemoved onOrderRemoved;

	void Awake()
	{
		GetComponentInParent<OMController_v2>().onOMDispatch += RemoveOrder;
		GetComponent<MeshRenderer>().enabled = false;
		GetComponentInChildren<ParticleSystem>().enableEmission = false;

		isBeingUsed = false;
	}

	void Start()
	{
		// We consider the future case in which there will be multiple orders to select from...
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Order");
		foreach(GameObject go in gos)
		{
			Order_v2 o = go.GetComponent<Order_v2>();
			if (o != null)
			{
				o.onOrderDropped += HandleOrderDropped;
				o.onOrderGrabbed += HandleOrderGrabbed;
			}
		}
	}

	void HandleOrderSlotted()
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
				order.onOrderDropped += HandleOrderSlotted;
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
				order.onOrderDropped -= HandleOrderSlotted;
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

	void HandleOrderGrabbed()
	{
		GetComponent<MeshRenderer>().enabled = true;
		GetComponentInChildren<ParticleSystem>().enableEmission = true;
	}

	void HandleOrderDropped()
	{
		GetComponent<MeshRenderer>().enabled = false;
		GetComponentInChildren<ParticleSystem>().enableEmission = false;
	}
}
