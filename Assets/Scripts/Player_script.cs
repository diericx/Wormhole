using UnityEngine;
using System.Collections;

public class Player_script : MonoBehaviour {

    private float MOVE_TO_LOOK_SPD = 30f;
    private float Z_GRAVITY = 18f;
    private float Y_GRAVITY = -9f;
    private float VELOCITY_LIMIT_Y = 99f;
    private float VELOCITY_LIMIT_Z = 10f;
    Rigidbody rb;

	//game objects
	public GameObject spawn;
    
    //prefabs
    public GameObject ring_prefab;

	//scripts
    public CubeSpawner cubeSpawner_script;

	//variables
    public static float sector = 0;
	
	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        //cc = gameObject.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //start Game
		if (Input.GetKey(KeyCode.Space)) {
            GameController_script.start = true;
		}

        //game start and restart
        if (GameController_script.start)
        {
            Physics.gravity = new Vector3(0, Y_GRAVITY, Z_GRAVITY);
            //move where you are looking
            rb.AddRelativeForce(Vector3.forward * MOVE_TO_LOOK_SPD);
        }
        else
        {
            Physics.gravity = new Vector3(0, Y_GRAVITY, 0);
        }


        //Spinning the cubes
        if (Input.GetKey(KeyCode.E))
        {
            CubeHolder_script.rotateRight();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            CubeHolder_script.rotateLeft();
        }


        //LIMIT velocity z
        if (rb.velocity.z > VELOCITY_LIMIT_Z)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, VELOCITY_LIMIT_Z);
        }
        //LIMIT velocity y
        if (rb.velocity.y > VELOCITY_LIMIT_Y)
        {
            rb.velocity = new Vector3(rb.velocity.x, 8.5f, rb.velocity.z);
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
			//cc.Move(Vector3.zero * Time.deltaTime);
		}
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "walkable")
        {
            if (GameController_script.start)
            {
            	Instantiate(ring_prefab, transform.position, Quaternion.identity);	
                rb.AddForce(new Vector3(0, 500, 0));
            }
        } else
        {            
            transform.position = spawn.transform.position;
            rb.velocity = new Vector3(0, 0, 0);
            GameController_script.start = false;
            sector = 0;
            
			cubeSpawner_script.resetCubes();
        }
    }

}
