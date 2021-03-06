﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartIngredientSpawner_v2 : MonoBehaviour {

	public delegate void OnSpawn();

	public event OnSpawn onSpawn;

	public Transform spawnPoint;

	public Vector3 minSpawnForce;

	public Vector3 maxSpawnForce;

	public GameObject spawnPrefab;

	public int maxSpawned;

	public int spawnPerSec;

	public Animation spawnAnimation;

	private List<GameObject> unusedIngsPool;
	private List<GameObject> inUseIngsPool;

	private bool isSpawning, isFirstSpawn;

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
			ing.GetComponent<Ingredient_v2>().OnDispatch += Restore;
			ing.GetComponent<Ingredient_v2>().OnOutOfRange += Restore;
			unusedIngsPool.Add(ing);
		}

		isFirstSpawn = true;
	}

	void OnDestroy()
	{
		// foreach (GameObject go in unusedIngsPool)
		// {
		// 	Ingredient_v2 ing = go.GetComponent<Ingredient_v2>();
		// 	if (ing != null)
		// 	{
		// 		ing.OnDispatch -= Restore;
		// 		ing.OnOutOfRange -= Restore;
		// 	}
		// }
	}

	void Update()
	{
		if ((!isSpawning) && (inUseIngsPool.Count < maxSpawned))
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
			inUseIngsPool.Add(ing);

			if (onSpawn != null && !isFirstSpawn)
				onSpawn.Invoke();
			
			float animTime = 0.0f;
			if (spawnAnimation != null)
			{
				spawnAnimation.Play();
				animTime = spawnAnimation.clip.length;
			}

			yield return new WaitForSeconds(Mathf.Max(1.0f/spawnPerSec + Random.Range(0.0f,0.5f), animTime));
		}

		unusedIngsPool.Clear();
		isSpawning = false;
		isFirstSpawn = false;
	}

	void Restore(Ingredient_v2 ing)
	{
		if (inUseIngsPool.Contains(ing.gameObject))
		{
			inUseIngsPool.Remove(ing.gameObject);
			ing.gameObject.SetActive(false);
			ing.gameObject.transform.parent = null;
			ing.gameObject.transform.position = spawnPoint.position;
			ing.gameObject.transform.rotation = spawnPoint.rotation;
			unusedIngsPool.Add(ing.gameObject);
		}
		else
			Debug.Log("An object restore event arrived for an object that doesn't exist in the spawner's pool of objects...");
	}
}
