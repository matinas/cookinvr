using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingAreaController_v2 : MonoBehaviour {

	public float respawnTime;

	void OnTriggerExit(Collider col)
	{
		Ingredient_v2 ing = col.GetComponent<Ingredient_v2>();
		if (ing != null)
			StartCoroutine(Restore(ing));
	}

	IEnumerator Restore(Ingredient_v2 ing)
	{
		yield return new WaitForSeconds(respawnTime);

		ing.OutOfRangeRestore();
	}
}
