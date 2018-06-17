using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager_v2 : MonoBehaviour {

	public int initOrder;

	private int actualOrder;

	public static OrderManager_v2 instance = null;

	void Awake()
	{
		if (instance == null)		 // If instance do not exist, set instance to this
			instance = this; 
		else
			if (instance != this)    // If instance already exists and it's not this...
				Destroy(gameObject); // Destroy this. This enforces the Singleton pattern (there can only ever be one instance).

		// Sets this object to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

		actualOrder = initOrder;
	}

	public int GetNextOrderNum()
	{
		return actualOrder++;
	}
}
