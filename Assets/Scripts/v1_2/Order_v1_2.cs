using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Order_v1_2 : MonoBehaviour {

	public delegate void OnOrderGrabbed();
	public delegate void OnOrderDropped();

	[HideInInspector]
	public event OnOrderGrabbed onOrderGrabbed;
	[HideInInspector]
	public event OnOrderDropped onOrderDropped;

	public Recipe_v1_2 recipe { get; private set; }
	public int num { get; private set; }

	void Start()
	{
		num = OrderManager_v1_2.instance.GetNextOrderNum();
		Debug.Log("Order " + num + " arrived. Put your hands to work!");

		recipe = RecipeManager_v1_2.instance.GetRandomRecipe();
		if (recipe != null)
			Debug.Log("The assigned recipe is: " + recipe.AsString());
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
