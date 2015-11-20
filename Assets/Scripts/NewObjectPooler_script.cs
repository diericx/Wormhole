using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewObjectPooler_script : MonoBehaviour {

	public static NewObjectPooler_script current;
	public GameObject pooledObject;
	public int pooledAmount = 50;
	public bool willGrow = true;
	
	List<GameObject> pooledObjects;	

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>();
		
		for (int i = 0; i < pooledAmount; i ++) {
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}
	
	public GameObject GetPooledObject() {
	
		for (int i = 0; i < pooledObjects.Count; i ++) {
			if (!pooledObjects[i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}
		
		if (willGrow) {
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
			return obj;
		}
		
		return null;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
