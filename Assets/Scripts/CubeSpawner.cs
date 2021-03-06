﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour {

//    public GameObject cubePrefab;
//	public GameObject walk_cubePrefab;
	public GameObject cubePooler_object;
	
	private NewObjectPooler_script cubePooler_script;
	private float width = 10.0f;
	private float initLength = 10.0f;
	private int amountPerSpawn = 2;
	private int spawnDistance = 100;

    public static Dictionary<int, List<GameObject>> cubes;

    private int LOOKAHEAD = 10;
    private int SECTOR_SIZE = 10;
    private float CIRCLE_RAD = 15f;
    private float CUBE_SPAWN_MODIFIER = 0.5f;

    // Use this for initialization
    void Start () {
        cubes = new Dictionary<int, List<GameObject>>();
        
        cubePooler_script = cubePooler_object.GetComponent<NewObjectPooler_script>();
        //spawnCubes(10f, 10f, 10f);
        //for (int i = 0; i < 1; i ++)
        //{
        //    print("test");
        //    spawnCubes(10f, 10f, i * 10f);
        //}

    }

	// Update is called once per frame
	void Update () {
        //print(Player_script.sector);
        //if ( !cubes.ContainsKey((int)Player_script.sector) )
        //{
        //    print("Need to spawn!");
        //}
        checkAndSpawnAhead();
    }

    void checkAndSpawnAhead()
    {
        int start = (int)Player_script.sector;
        for (int i = start; i < start + LOOKAHEAD; i ++)
        {
            if (!cubes.ContainsKey(i))
            {
                print("NEED TO SPAWN, " + cubes.Count);
                cubes.Add(i, new List<GameObject>());
                spawnCubes(10, 10, i);
            }
        }
    }


    void spawnCubes(float length, float width, float offset)
    {
        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < amountPerSpawn; k++)
                {
                    var currentPos = new Vector3(0, 0, 0);
                    var targetPos = new Vector3(0, 0, 0);

                    //if (i == 0)
                    //{
                    //    //bottom
                    //    var randX = Random.Range(-width, width);
                    //    var randY = 0;
                    //    var randZ = Random.Range((float)j, (float)j + 1) + offset* SECTOR_SIZE;

                    //    currentPos = new Vector3(randX, randY + spawnDistance, randZ);
                    //    targetPos = new Vector3(randX, randY, randZ);
                    //}
                    //else if (i == 1)
                    //{
                    //    //right
                    //    var randX = width;
                    //    var randY = Random.Range(0, width * 2);
                    //    var randZ = Random.Range((float)j, (float)j + 1) + offset * SECTOR_SIZE;

                    //    currentPos = new Vector3(randX, randY + spawnDistance, randZ);
                    //    targetPos = new Vector3(randX, randY, randZ);
                    //}
                    //else if (i == 2)
                    //{
                    //    //top
                    //    var randX = Random.Range(-width, width);
                    //    var randY = width * 2;
                    //    var randZ = Random.Range((float)j, (float)j + 1) + offset * SECTOR_SIZE;

                    //    currentPos = new Vector3(randX, randY + spawnDistance, randZ);
                    //    targetPos = new Vector3(randX, randY, randZ);
                    //}
                    //else if (i == 3)
                    //{
                    //    //right
                    //    var randX = -width;
                    //    var randY = Random.Range(0, width * 2);
                    //    var randZ = Random.Range((float)j, (float)j + 1) + offset * SECTOR_SIZE;

                    //    currentPos = new Vector3(randX, randY + spawnDistance, randZ);
                    //    targetPos = new Vector3(randX, randY, randZ);
                    //}

                    //Get Object position based off of random position on circle
                    var randAngle = Random.Range(1f, 360f);
                    var angle_rad = randAngle * Mathf.Rad2Deg;
                    var x = Mathf.Cos(angle_rad) * CIRCLE_RAD;
                    var y = Mathf.Sin(angle_rad) * CIRCLE_RAD;
                    var z = Random.Range((float)j, (float)j + 1) + offset * SECTOR_SIZE;
                    //set postiion
                    targetPos = new Vector3(x, y, z);
                    currentPos = targetPos + new Vector3(0, spawnDistance, 0);
                    //spawn cube
                    GameObject newCube = cubePooler_script.GetPooledObject();
					//newCube.transform.position = currentPos;
                    //newCube.transform.localScale = new Vector3(1 + Random.Range(0.01f, 0.2f), 1 + Random.Range(0.01f, 0.2f), 1 + Random.Range(0.01f, 0.2f));

                    //set variables in cube's script
                    Cube_script cubeScript = newCube.transform.GetChild(0).GetComponent<Cube_script>();
					cubeScript.setTargetObjPos(targetPos);
					newCube.transform.GetChild(0).transform.position = currentPos;

                    cubeScript.waitTime = 0.1f * j + 1 * k + 1;

                    if (offset <= 9)
                    {
                        cubeScript.waitTime += offset;
                    }

                    cubeScript.waitTime = cubeScript.waitTime * CUBE_SPAWN_MODIFIER;

                    //set cube to active
                    newCube.SetActive(true);
					
                    //add cube to list
					cubes[(int)offset].Add(newCube);
                }
            }
        }
    }
    
    public void resetCubes() {
    	cubePooler_script.disableAll();
		cubes = new Dictionary<int, List<GameObject>>();
    }
}
//
//public class Cube : MonoBehaviour
//{
//    public GameObject cubeObj;
//    public Vector3 targetPos;
//	private float randScale = 0f;
//	private int posChanger = 0;
//	private bool isWalkable = false;
//
//	public Cube(GameObject cubePrefab, GameObject walk_cubePrefab, Vector3 currentPos, Vector3 targetPos, float waitTime)
//    {
//        this.targetPos = targetPos;
//
//		var scaleDecider = Random.Range (1, 100);
//		if (scaleDecider >= 1 && scaleDecider <= 70) {
//			//small
//			randScale = Random.Range (1f, 1.5f);
//		} else if (scaleDecider >= 71 && scaleDecider <= 80) {
//			//medium
//			randScale = Random.Range(2f, 2.5f);
//		} else if (scaleDecider >= 81 && scaleDecider <= 100) {
//			//large walkable
//			isWalkable = true;
//			posChanger = 1;
//			randScale = Random.Range(3f, 4f);
//		}
//
//		currentPos = new Vector3 (currentPos.x, currentPos.y + posChanger, currentPos.z);
//		targetPos = new Vector3 (targetPos.x, targetPos.y + posChanger, targetPos.z);
//
//		if (isWalkable) {
//			this.cubeObj = (GameObject)Instantiate(walk_cubePrefab, currentPos, Quaternion.identity);
//		} else {
//			this.cubeObj = (GameObject)Instantiate(cubePrefab, currentPos, Quaternion.identity);
//		}
//
//		Vector3 scale = new Vector3 (randScale, randScale, randScale + 0.00012f);
//
//		Cube_script cubeScript = (Cube_script)cubeObj.GetComponent ("Cube_script");
//		cubeScript.waitTime = waitTime;
//		cubeScript.startDrop ();
//		cubeScript.setTargetObjPos(targetPos);
//
//		cubeObj.transform.localScale = scale;
//    }
//}
