using UnityEngine;
using System.Collections;

public class Cube_script : MonoBehaviour {

	public float waitTime = 0f;
	public bool shouldStart = false;
	public bool isWalkable = false;

    private float CUBE_MOVE_SPEED = 5f;

    private Transform targetObj;

    IEnumerator waitFor(){
		//functionality here
		yield return new WaitForSeconds(waitTime);

		shouldStart = true;
	}
	
	void OnEnable() {
		StartCoroutine(waitFor());
		randomizeCubeAppearence();
	}
	
	void OnDisable() {
		shouldStart = false;
		waitTime = 0f;
		isWalkable = false;
	}
	
	void randomizeCubeAppearence() {
		var scaleDecider = Random.Range (1, 100);
		float randScale = 1;
		
		if (scaleDecider >= 1 && scaleDecider <= 70) {
			//small
			randScale = Random.Range (1f, 1.5f);
            this.transform.tag = "!walkable";
        } else if (scaleDecider >= 71 && scaleDecider <= 90) {
			//medium
			randScale = Random.Range(2f, 2.5f);
            this.transform.tag = "!walkable";
        } else if (scaleDecider >= 91 && scaleDecider <= 100) {
			//large walkable
			isWalkable = true;
            this.transform.tag = "walkable";
			randScale = Random.Range(3f, 4f);
		}
		
		if (transform.tag == "walkable") {
			this.GetComponent<Renderer>().material.color = Color.green;
		} else {
			this.GetComponent<Renderer>().material.color = Color.grey;
		}
		
		Vector3 scale = new Vector3 (randScale, randScale, randScale + 0.00012f);
		
		this.transform.localScale = scale;

        NewObjectPooler_script.randomScale(this.gameObject);

    }

	// Use this for initialization
	void Start () {
        targetObj = transform.parent;
    }

	public void startDrop() {
		StartCoroutine(waitFor());
	}

    public void setTargetObjPos(Vector3 pos)
    {
        transform.parent.position = pos;
        //targetObj.position = pos;
    } 
	
	// Update is called once per frame
	void Update () {
		if (transform.position != targetObj.position && shouldStart == true) {
			var newX = (transform.position.x* CUBE_MOVE_SPEED + targetObj.position.x)/ (CUBE_MOVE_SPEED + 1);
			var newY = (transform.position.y* CUBE_MOVE_SPEED + targetObj.position.y)/ (CUBE_MOVE_SPEED + 1);
			var newZ = (transform.position.z* CUBE_MOVE_SPEED + targetObj.position.z)/ (CUBE_MOVE_SPEED + 1);

			var newPos = new Vector3(newX, newY, newZ);
			transform.position = newPos;

		}

        //have cube always look at center
        var centerPosition = new Vector3(0, 0, transform.position.z);
        transform.LookAt(centerPosition);
    }
}
