using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewObjectPooler_script : MonoBehaviour {

	public static NewObjectPooler_script current;
	public GameObject pooledObject;
    private GameObject cubeHolder;
	public int pooledAmount = 50;
	public bool willGrow = true;
	
	List<GameObject> pooledObjects;	

	// Use this for initialization
	void Start () {
        cubeHolder = GameObject.Find("CubeHolder");
		pooledObjects = new List<GameObject>();
		
		for (int i = 0; i < pooledAmount; i ++) {
            //spawn cube holder and cube
			GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = cubeHolder.transform;
            //put cube above holder
            var cube = obj.transform.GetChild(0);
            cube.transform.rotation = cubeHolder.transform.rotation;
            cube.transform.Translate(Vector3.up * 100);
            //cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y + 50, cube.transform.position.z);
            //scale cube randomle
            randomScale(obj.transform.GetChild(0).gameObject);
            //set it actibve
            obj.SetActive(false);
            //add it to the pool
			pooledObjects.Add(obj);
		}
	}

    public void disableAll()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            print("disable");
            pooledObjects[i].SetActive(false);
        }
    }

	public GameObject GetPooledObject() {
	
		for (int i = 0; i < pooledObjects.Count; i ++) {
			if (!pooledObjects[i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}
		
		if (willGrow) {
            //spawmn and set parent
			GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = cubeHolder.transform;
            //postiion cube above parent
            var cube = obj.transform.GetChild(0);
            cube.transform.rotation = cubeHolder.transform.rotation;
            cube.transform.Translate(Vector3.up * 100);
            //cube.transform.position = new Vector3(cube.transform.localPosition.x, cube.transform.localPosition.y + 50, cube.transform.localPosition.z);

            randomScale(obj);
            obj.SetActive(false);
			pooledObjects.Add(obj);
			return obj;
		}
		
		return null;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void randomScale(GameObject go)
    {
        go.transform.localScale = new Vector3(go.transform.localScale.x+Random.Range(0.01f, 0.1f), go.transform.localScale.y+Random.Range(0.01f, 0.1f), go.transform.localScale.z+Random.Range(0.01f, 0.1f));
    }
}
