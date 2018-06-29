using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner_v2 : MonoBehaviour {

	public Transform orderPrefab;

	public OMController_v2 OMController;

	public Transform spawnPoint;

	void Awake()
	{
		SpawnNewOrder(0);
		OMController.onOMDispatch += SpawnNewOrder;
	}

	void SpawnNewOrder(int score)
	{
		GameObject.Instantiate(orderPrefab, spawnPoint.position, spawnPoint.rotation);
	}

}
