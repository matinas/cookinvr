using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartIngredientSpawner_v2 : MonoBehaviour {

	public Transform spawnPoint;

	public Vector3 minSpawnForce;

	public Vector3 maxSpawnForce;

	public GameObject spawnPrefab;

	public int maxSpawned;

	public int spawnPerSec;

	private List<GameObject> unusedIngsPool;
	private List<GameObject> inUseIngsPool;

	private bool isSpawning;

	void Awake()
	{
		unusedIngsPool = new List<GameObject>();
		inUseIngsPool = new List<GameObject>();
		isSpawning = false;
	
		// Instanciate object pool
		for (int i=0; i<maxSpawned; i++)
		{
			GameObject ing = GameObject.Instantiate(spawnPrefab,spawnPoint.position,spawnPoint.rotation);
			ing.SetActive(false);
			unusedIngsPool.Add(ing);
		}
	}

	void Update()
	{
		if ((!isSpawning) && (inUseIngsPool.Count == 0))
			StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		isSpawning = true;

		for (int i=0; i<unusedIngsPool.Count; i++)
		{
			Vector3 spawnForce = new Vector3(Random.Range(minSpawnForce.x,maxSpawnForce.x),
											 Random.Range(minSpawnForce.y,maxSpawnForce.y),
											 Random.Range(minSpawnForce.z,maxSpawnForce.z));
											 
			GameObject ing = unusedIngsPool[i];
			ing.SetActive(true);
			ing.GetComponent<Rigidbody>().AddForce(spawnForce);
			unusedIngsPool.RemoveAt(i);
			inUseIngsPool.Add(ing);

			yield return new WaitForSeconds(1.0f/spawnPerSec + Random.Range(0.0f,0.5f));
		}

		isSpawning = false;
	}
}
