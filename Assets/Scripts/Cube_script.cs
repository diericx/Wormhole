using UnityEngine;
using System.Collections;

public class Cube_script : MonoBehaviour {

	public Vector3 targetPos = new Vector3 (0, 0, 0);
	public float waitTime = 0f;
	public bool shouldStart = false;

	IEnumerator waitFor(){
		//functionality here
		yield return new WaitForSeconds(waitTime);

		shouldStart = true;
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
