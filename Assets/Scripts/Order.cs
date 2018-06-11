using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Order : MonoBehaviour {

	public delegate void OnOrderGrabbed();
	public delegate void OnOrderDropped();

	private bool orderInserted = false;

	[HideInInspector]
	public event OnOrderGrabbed onOrderGrabbed;
	[HideInInspector]
	public event OnOrderDropped onOrderDropped;

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
