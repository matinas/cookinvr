using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe_v2 : MonoBehaviour {

	public string recipeName;
	public List<string> ingredients;

	public Transform disassembledPrefab;
	public Transform assembledPrefab;

	void Awake()
	{
		if (recipeName == null)
			recipeName = "Unknown";
	}

	public string AsString()
	{
		int count = ingredients.Count;
		string str = count > 0 ? recipeName + ": " : recipeName;

		for(int i=0; i<count; i++)
		{
			str += i<(count-1) ? ingredients[i] + " - " : ingredients[i];
		}

		return str;
	}
}
