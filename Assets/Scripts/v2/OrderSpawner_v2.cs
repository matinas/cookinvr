using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner_v2 : MonoBehaviour {

	public Transform orderPrefab;

	public OMController_v2 OMController;

	void Awake()
	{
		OMController.onOMDispatch += SpawnNewOrder;
	}

	void SpawnNewOrder(int score)
	{
		GameObject.Instantiate(orderPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
	}

}
