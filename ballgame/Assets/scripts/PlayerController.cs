using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float inputType;
	public float speed;
    public float rotspeed;
    public float camspeed;
    public bool enableY;
    public bool invertX;
    public bool invertY;
    public bool lockCannonRotation;
    public GameObject originPoint;
    public GameObject cameraPitch;
    public GameObject cameraYaw;
    public GameObject cannon;
    public GameObject projectile;
	
	private Rigidbody rb;
	
	void Start ()
	{
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
    void Update() {
        originPoint.transform.position = transform.position;

        //Shooting
        if (Input.GetButtonDown("Fire1")) {
            Vector3 spawnposition = new Vector3(cannon.transform.position.x, cannon.transform.position.y + 0.9f, cannon.transform.position.z);
            Rigidbody instantiatedProjectile = Instantiate(projectile, spawnposition, cameraPitch.transform.rotation) as Rigidbody;
            print(rb.velocity);
            instantiatedProjectile.velocity = rb.velocity;
        }
    }

	void FixedUpdate ()
	{
		//Set rotation values
        float ballX = 0;
        float ballY = Input.GetAxis("MoveUp");
        float camX = 0;
        float camY = 0;
        float strafe = 0;
        if (enableY == true)
        {
            camY = Input.GetAxis("CamY");
        }
        if (invertX == true)
        {
            camX *= -1;
        }
        if (invertY == true)
        {
            camY *= -1;
        }
        if (inputType == 1) {
            ballX = Input.GetAxis("Horizontal");
            camX = Input.GetAxis("CamX");
        }
        if (inputType == 2) {
            ballX = Input.GetAxis("CamX");
            strafe = Input.GetAxis("Horizontal");
        }

        //Set rotations
        originPoint.transform.Rotate(0 * rotspeed, ballX * rotspeed, 0);
        if (lockCannonRotation == true && inputType == 1) {
            cameraYaw.transform.Rotate(0 * rotspeed, (ballX * -1) * rotspeed, 0);
        }
        transform.rotation = originPoint.transform.rotation;
        transform.Rotate(-90, 0, 0);
        cannon.transform.rotation = cameraPitch.transform.rotation;
        cannon.transform.Rotate(-90, 0, 0);

        //Add force in forward facing direction
        rb.AddForce(Input.GetAxis ("Vertical") * originPoint.transform.forward * speed);
        rb.AddForce(strafe * originPoint.transform.right * speed);
        rb.AddForce(ballY * originPoint.transform.up * speed);

        //Braking
        if(Input.GetAxis("Brake") < -0.5) {
            rb.velocity = rb.velocity * 0.95f;
        }

        //Camera rotation
        cameraYaw.transform.Rotate(0, camX * camspeed, 0);
        if(cameraPitch.transform.eulerAngles.x <= 30 || cameraPitch.transform.eulerAngles.x >= 330) {
            cameraPitch.transform.Rotate(camY * camspeed, 0, 0);
        }
        if (cameraPitch.transform.eulerAngles.x >= 30 && cameraPitch.transform.eulerAngles.x <= 320) {
            cameraPitch.transform.localEulerAngles = new Vector3(29.999f,
            cameraPitch.transform.localEulerAngles.y,
            cameraPitch.transform.localEulerAngles.z
            );
        }
        if (cameraPitch.transform.eulerAngles.x >= 40 && cameraPitch.transform.eulerAngles.x <= 330) {
            cameraPitch.transform.localEulerAngles = new Vector3(331.001f,
            cameraPitch.transform.localEulerAngles.y,
            cameraPitch.transform.localEulerAngles.z
            );
        }
    }
}