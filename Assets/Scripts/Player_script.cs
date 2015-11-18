using UnityEngine;
using System.Collections;

public class Player_script : MonoBehaviour {
	
	CharacterController cc;
	public GameObject spawn;

    public static float sector = 0;
	
	// Use this for initialization
	void Start () {
		cc = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<Rigidbody>().AddForce(Vector3.down * 10);
		if (Input.GetKey(KeyCode.LeftShift)) {
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			//Vector3 up = transform.TransformDirection(Vector3.up);
			cc.Move((forward * 10) * Time.deltaTime);
			//cc.Move((up * 50) * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.E)) {
			Debug.Log ("E PRESSED");
			Physics.gravity = new Vector3(9.81f, 0, 0);
		}

        //set sector to a new value if the player has moved forward a sector
        float currentSector = Mathf.Floor(this.transform.position.z / 10);
        if (currentSector > 0 && currentSector > sector)
        {
            sector = currentSector;
        }
    }
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.transform.tag == "Respawn") {
			transform.position = spawn.transform.position;
			cc.Move(Vector3.zero * Time.deltaTime);
		}
	}
	
}
