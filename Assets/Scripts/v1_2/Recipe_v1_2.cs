using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe_v1_2 : MonoBehaviour {

	public string recipeName;

	public Transform disassembledPrefab;
	public Transform assembledPrefab;

	void Awake()
	{
		if (recipeName == null)
			recipeName = "Unknown";
	}

	public string AsString()
	{
		return recipeName;
	}
}
