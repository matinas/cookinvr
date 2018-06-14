using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner : MonoBehaviour {

	public Transform orderPrefab;

	public OMController OMController;

	void Awake()
	{
		OMController.onOMDispatchSuccess += SpawnNewOrder;
	}

	void SpawnNewOrder()
	{
		GameObject.Instantiate(orderPrefab, transform.position + new Vector3(0,1,0), transform.rotation);
	}

}
