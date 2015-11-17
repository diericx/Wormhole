using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {

    public GameObject cubePrefab;
	public GameObject walk_cubePrefab;
	private float width = 10.0f;
	private float initLength = 10.0f;
	private int amountPerSpawn = 2;
	private int spawnDistance = 100;


	// Use this for initialization
	void Start () {
        spawnCubes(10f, 10f, 10f);
        //for (int i = 0; i < 1; i ++)
        //{
        //    print("test");
        //    spawnCubes(10f, 10f, i * 10f);
        //}

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

                    if (i == 0)
                    {
                        //bottom
                        var randX = Random.Range(-width, width);
                        var randY = 0;
                        var randZ = Random.Range((float)j, (float)j + 1) + offset;

                        currentPos = new Vector3(randX, randY + spawnDistance, randZ);
                        targetPos = new Vector3(randX, randY, randZ);
                    }
                    else if (i == 1)
                    {
                        //right
                        var randX = width;
                        var randY = Random.Range(0, width * 2);
                        var randZ = Random.Range((float)j, (float)j + 1) + offset;

                        currentPos = new Vector3(randX, randY + spawnDistance, randZ);
                        targetPos = new Vector3(randX, randY, randZ);
                    }
                    else if (i == 2)
                    {
                        //top
                        var randX = Random.Range(-width, width);
                        var randY = width * 2;
                        var randZ = Random.Range((float)j, (float)j + 1) + offset;

                        currentPos = new Vector3(randX, randY + spawnDistance, randZ);
                        targetPos = new Vector3(randX, randY, randZ);
                    }
                    else if (i == 3)
                    {
                        //right
                        var randX = -width;
                        var randY = Random.Range(0, width * 2);
                        var randZ = Random.Range((float)j, (float)j + 1) + offset;

                        currentPos = new Vector3(randX, randY + spawnDistance, randZ);
                        targetPos = new Vector3(randX, randY, randZ);
                    }


                    var newCube = new Cube(cubePrefab, walk_cubePrefab, currentPos, targetPos, 0.1f * j + 1 * k + 1);
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

public class Cube : MonoBehaviour
{
    public GameObject cubeObj;
    public Vector3 targetPos;
	private float randScale = 0f;
	private int posChanger = 0;
	private bool isWalkable = false;

	public Cube(GameObject cubePrefab, GameObject walk_cubePrefab, Vector3 currentPos, Vector3 targetPos, float waitTime)
    {
        this.targetPos = targetPos;

		var scaleDecider = Random.Range (1, 100);
		if (scaleDecider >= 1 && scaleDecider <= 70) {
			//small
			randScale = Random.Range (1f, 1.5f);
		} else if (scaleDecider >= 71 && scaleDecider <= 80) {
			//medium
			randScale = Random.Range(2f, 2.5f);
		} else if (scaleDecider >= 81 && scaleDecider <= 100) {
			//large walkable
			isWalkable = true;
			posChanger = 1;
			randScale = Random.Range(3f, 4f);
		}

		currentPos = new Vector3 (currentPos.x, currentPos.y + posChanger, currentPos.z);
		targetPos = new Vector3 (targetPos.x, targetPos.y + posChanger, targetPos.z);

		if (isWalkable) {
			this.cubeObj = (GameObject)Instantiate(walk_cubePrefab, currentPos, Quaternion.identity);
		} else {
			this.cubeObj = (GameObject)Instantiate(cubePrefab, currentPos, Quaternion.identity);
		}

		Vector3 scale = new Vector3 (randScale, randScale, randScale + 0.00012f);

		Cube_script cubeScript = (Cube_script)cubeObj.GetComponent ("Cube_script");
		cubeScript.waitTime = waitTime;
		cubeScript.startDrop ();
		cubeScript.targetPos = targetPos;

		cubeObj.transform.localScale = scale;
    }
}
