using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleIngredientSpawner_v2 : MonoBehaviour {

	public Transform spawnPoint;

	public Vector3 minSpawnForce;

	public Vector3 maxSpawnForce;

	public GameObject spawnPrefab;

	public int maxSpawned;

	public int spawnPerSec;

	private int currentSpawned;

	private bool isSpawning;

	void Awake()
	{
		currentSpawned = 0;
		isSpawning = false;
	}

	void Update()
	{
		if ((!isSpawning) && (currentSpawned < maxSpawned))
			StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		isSpawning = true;

		while (currentSpawned < maxSpawned)
		{
			GameObject go = GameObject.Instantiate(spawnPrefab,spawnPoint.position,spawnPoint.rotation);

			Vector3 spawnForce = new Vector3(Random.Range(minSpawnForce.x,maxSpawnForce.x),
											 Random.Range(minSpawnForce.y,maxSpawnForce.y),
											 Random.Range(minSpawnForce.z,maxSpawnForce.z));
											 
			go.GetComponent<Rigidbody>().AddForce(spawnForce);
			currentSpawned++;

			yield return new WaitForSeconds(1.0f/spawnPerSec + Random.Range(0.0f,0.5f));
		}

		isSpawning = false;
	}
}
