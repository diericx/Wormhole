using UnityEngine;
using System.Collections;

public class Cube_script : MonoBehaviour {

	public Vector3 targetPos = new Vector3 (0, 0, 0);
	public float waitTime = 0f;
	public bool shouldStart = false;
	public bool isWalkable = false;

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
		targetPos = new Vector3();
	}
	
	void randomizeCubeAppearence() {
		var scaleDecider = Random.Range (1, 100);
		float randScale = 1;
		float posChanger;
		
		if (scaleDecider >= 1 && scaleDecider <= 70) {
			//small
			randScale = Random.Range (1f, 1.5f);
		} else if (scaleDecider >= 71 && scaleDecider <= 90) {
			//medium
			randScale = Random.Range(2f, 2.5f);
		} else if (scaleDecider >= 91 && scaleDecider <= 100) {
			//large walkable
			isWalkable = true;
			posChanger = 1;
			randScale = Random.Range(3f, 4f);
		}
		
		if (isWalkable) {
			this.GetComponent<Renderer>().material.color = Color.green;
		} else {
			this.GetComponent<Renderer>().material.color = Color.grey;
		}
		
		Vector3 scale = new Vector3 (randScale, randScale, randScale + 0.00012f);
		
		this.transform.localScale = scale;
		
	}

	// Use this for initialization
	void Start () {

	}

	public void startDrop() {
		StartCoroutine(waitFor());
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != targetPos && shouldStart == true) {
			var newX = (transform.position.x*10 + targetPos.x)/11;
			var newY = (transform.position.y*10 + targetPos.y)/11;
			var newZ = (transform.position.z*10 + targetPos.z)/11;

			var newPos = new Vector3(newX, newY, newZ);
			transform.position = newPos;
		}
	}
}
