using UnityEngine;
using System.Collections;

public class ring_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(0.1f, 0.0001f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3(0.1f, 0f, 0.1f);
		Color color = transform.GetComponent<Renderer>().material.color -= new Color(0,0,0, .02f);
	}

	
}
