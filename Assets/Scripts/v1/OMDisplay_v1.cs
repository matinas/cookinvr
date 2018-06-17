using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMDisplay_v1 : MonoBehaviour {

	[SerializeField]
	[Tooltip("Reference to the Display game object")]
	private Transform display;
	
	// Use this for initialization
	void Awake()
	{
		display.gameObject.SetActive(false);

		GetComponent<OMController_v1>().onOMPowerOn += ShowDisplay;
		GetComponent<OMController_v1>().onOMPowerOff += HideDisplay;
		GetComponentInParent<OMController_v1>().onOMDispatchSuccess += HideDisplay;
	}
	
	void ShowDisplay()
	{
		display.gameObject.SetActive(true);
	}

	void HideDisplay()
	{
		display.gameObject.SetActive(false);
	}
}
