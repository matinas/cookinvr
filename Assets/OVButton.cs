using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;

public class OVButton : MonoBehaviour {

	public event Action OnButtonClick = () => {};

	public Interactable button;

	void Awake()
	{
	}

}
