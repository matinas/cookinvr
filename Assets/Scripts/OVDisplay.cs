using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVDisplay : MonoBehaviour {

	[SerializeField]
	[Tooltip("Reference to the Display game object")]
	private Transform display;
	
	// Use this for initialization
	void Awake()
	{
		display.gameObject.active = false;

		GetComponent<OVController>().onOVPowerOn += ShowDisplay;
		GetComponent<OVController>().onOVPowerOff += HideDisplay;
	}
	
	void ShowDisplay()
	{
		display.gameObject.active = true;
	}

	void HideDisplay()
	{
		display.gameObject.active = false;
	}
}
