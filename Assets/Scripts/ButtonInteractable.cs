using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ButtonInteractable : MonoBehaviour {

	public event Action OnButtonPress = () => {};
	public event Action OnButtonRelease = () => {};

	public Transform buttonMax;

	private Vector3 iHandPos;
	private Vector3 iButtonPos;

	private bool buttonPressed;

	void Start()
	{
		buttonPressed = false;
		iButtonPos = transform.position;
	}

	void OnHandHoverBegin(Hand h)
	{
		iHandPos = h.transform.position;
	}

	void HandHoverUpdate(Hand h)
	{
		Vector3 offset = h.transform.position - iHandPos;
		Vector3 buttonPos = iButtonPos + new Vector3(0,offset.y,0);

		if (buttonPos.y <= buttonMax.position.y) // If the button reached the max pressed position, if it's still not pressed report the button press
		{
			if (!buttonPressed)
			{
				OnButtonPress();
				buttonPressed = true;
				transform.position = buttonMax.position;
			}
		}
		else
			if (buttonPos.y >= iButtonPos.y) // If the button reached the initial position, if it waiting for release (not pressed) report the release
			{
				if (buttonPressed)
				{
					OnButtonRelease();
					buttonPressed = false;
					transform.position = iButtonPos;
				}
			}
			else
				transform.position = buttonPos;
	}

	void OnHandHoverEnd(Hand h)
	{
		if (buttonPressed) // If the button was pressed and we lose contact, report the release and reset button (back to initial position)
		{
			OnButtonRelease();
			transform.position = iButtonPos;
			buttonPressed = false;
		}
		else
			transform.position = iButtonPos; // If the button wasn't fully pressed yet and we lose contact, just reset it
	}
}
