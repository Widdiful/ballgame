using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
    public float rotspeed;
    public GameObject cameraArm;
	
	private Rigidbody rb;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
        transform.Rotate(0, 0, rotation * rotspeed);
        //Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (Input.GetAxis ("Vertical") * cameraArm.transform.forward * speed);
	}
}