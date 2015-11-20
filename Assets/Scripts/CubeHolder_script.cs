using UnityEngine;
using System.Collections;

public class CubeHolder_script : MonoBehaviour {

    public static Vector3 targetRotation = new Vector3();

    private float ROTATE_SPEED = 20f;

	// Use this for initialization
	void Start () {
	
	}

    public static void rotateRight()
    {
        targetRotation = targetRotation + new Vector3(0, 0, -2);
        fixRotation();
    }
	
    public static void rotateLeft()
    {
        targetRotation = targetRotation + new Vector3(0, 0, 2);
        fixRotation();
    }

    static void fixRotation()
    {
        if (targetRotation.z < 0)
        {
            targetRotation = targetRotation + new Vector3(0, 0, 360);
        }
        else if (targetRotation.z > 360)
        {
            targetRotation = targetRotation + new Vector3(0, 0, -360);
        }
    }

	// Update is called once per frame
	void Update () {
        print(targetRotation + ", " + (transform.rotation.eulerAngles == targetRotation) );
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation.x, targetRotation.y, targetRotation.z), 0.07f);

        //if (transform.rotation.eulerAngles != targetRotation)
        //{
        //    var newX = 0;
        //    var newY = 0;
        //    var newZ = (transform.rotation.eulerAngles.z * ROTATE_SPEED + targetRotation.z) / (ROTATE_SPEED+1);

        //    var newRot = new Vector3(newX, newY, newZ);
        //    transform.rotation = Quaternion.Euler(newRot);

        //}
    }
}
