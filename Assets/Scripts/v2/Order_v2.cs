using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Order_v2 : MonoBehaviour {

	public delegate void OnOrderGrabbed();
	public delegate void OnOrderDropped();
	public delegate void OnOrderReady();

	[HideInInspector]
	public event OnOrderGrabbed onOrderGrabbed;
	[HideInInspector]
	public event OnOrderDropped onOrderDropped;
	[HideInInspector]
	public event OnOrderReady onOrderReady;

	public Recipe_v2 recipe { get; private set; }
	public int num { get; private set; }

	private Rigidbody rb;

	private bool readyNotify;

	void Start()
	{
		num = OrderManager_v2.instance.GetNextOrderNum();
		Debug.Log("Order " + num + " arrived. Put your hands to work!");

		recipe = RecipeManager_v2.instance.GetRandomRecipe();
		if (recipe != null)
			Debug.Log("The assigned recipe is " + recipe.ToString());

		rb = gameObject.GetComponent<Rigidbody>();

		readyNotify = false;
	}

	void Update()
	{
		if (rb.velocity == Vector3.zero)
		{
			if (!readyNotify)
			{
				readyNotify = true;
				if (onOrderReady != null)
					onOrderReady.Invoke();
			}
		}
	}

	void OnAttachedToHand(Hand h)
	{
		if (onOrderGrabbed != null)
			onOrderGrabbed.Invoke();
	}

	void OnDetachedFromHand(Hand h)
	{
		if (onOrderDropped != null)
			onOrderDropped.Invoke();
	}
}
