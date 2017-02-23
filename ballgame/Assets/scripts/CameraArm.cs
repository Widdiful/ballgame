using UnityEngine;
using System.Collections;

public class CameraArm : MonoBehaviour {

    public GameObject target;
    public int rotationspeed;
    public bool enableY;
    public bool invertX;
    public bool invertY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position;
	}

    void FixedUpdate() {
        float rotateX = Input.GetAxis("CamX");
        float rotateY = 0;
        if (enableY == true) {
            rotateY = Input.GetAxis("CamY");
        }
        if (invertX == true) {
            rotateX *= -1;
        }
        if (invertY == true)
        {
            rotateY *= -1;
        }

        transform.Rotate(rotateY * rotationspeed, rotateX * rotationspeed, 0);
    }
}
