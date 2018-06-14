using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMDisplay : MonoBehaviour {

	[SerializeField]
	[Tooltip("Reference to the Display game object")]
	private Transform display;
	
	// Use this for initialization
	void Awake()
	{
		display.gameObject.SetActive(false);

		GetComponent<OMController>().onOMPowerOn += ShowDisplay;
		GetComponent<OMController>().onOMPowerOff += HideDisplay;
		GetComponentInParent<OMController>().onOMDispatchSuccess += HideDisplay;
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
