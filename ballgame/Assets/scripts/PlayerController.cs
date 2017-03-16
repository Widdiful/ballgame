using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float inputType;
    public float ballType;
	public float speed;
    public float rotspeed;
    public float camspeed;
    public int vulcanMax;
    public int gunCooldown;
    public int points = 0;
    public bool enableY;
    public bool invertX;
    public bool invertY;
    public bool lockCannonRotation;
    public GameObject originPoint;
    public GameObject cameraPitch;
    public GameObject cameraYaw;
    public GameObject cannon;
    public GameObject cannon_k;
    public GameObject projectile;
    public GameObject vulcanProj;
    public GameObject shot0; //Single cannon
    public GameObject shot1; //Double cannon 1
    public GameObject shot2; //Double cannon 2
    public GameObject shot3; //Vulcan 1
    public GameObject shot4; //Vulcan 2
    public Scrollbar ammo;
    public Scrollbar cooldown;
    public Text pointsDisplay;
    public ParticleSystem afterburner1;
    public ParticleSystem afterburner2;
    public ParticleSystem afterburner3;
    public ParticleSystem afterburner4;

    private Rigidbody rb;
    private int shotNo = 0;
    private int vulcNo = 0;
    private float shotCooldown = 0;
    private float vulcanCooldown = 0;
    private float vulcanAmmo;
	
	void Start ()
	{
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (ballType == 1) {
            cannon_k.SetActive(false);
        }
        else if (ballType == 2) {
            cannon.SetActive(false);
        }
        vulcanAmmo = vulcanMax;
	}
	
    void Update() {
        originPoint.transform.position = transform.position;
        Vector3 spawnposition = new Vector3(0, 0, 0);
        shotCooldown -= 0.01f;
        vulcanCooldown -= 0.01f;
        //Shooting
        if (Input.GetButtonDown("Fire1")) {
            if (shotCooldown <= 0) {
                if (ballType == 1)
                {
                    spawnposition = new Vector3(shot0.transform.position.x, shot0.transform.position.y, shot0.transform.position.z);
                }
                else if (ballType == 2)
                {
                    switch (shotNo)
                    {
                        case 0:
                            spawnposition = new Vector3(shot1.transform.position.x, shot1.transform.position.y, shot1.transform.position.z);
                            shotNo = 1;
                            break;
                        case 1:
                            spawnposition = new Vector3(shot2.transform.position.x, shot2.transform.position.y, shot2.transform.position.z);
                            shotNo = 0;
                            break;
                    }
                }
                shotCooldown = gunCooldown;
            GameObject instantiatedProjectile = Instantiate(projectile, spawnposition, cameraPitch.transform.rotation) as GameObject;
            instantiatedProjectile.GetComponent<Rigidbody>().AddForce(rb.velocity * rb.velocity.magnitude);
            }
        }
        if (Input.GetButton("Fire2"))
        {
            if (vulcanCooldown <= 0 && vulcanAmmo > 0)
            {
                switch (vulcNo)
                {
                    case 0:
                        spawnposition = new Vector3(shot3.transform.position.x, shot3.transform.position.y, shot3.transform.position.z);
                        vulcNo = 1;
                        break;
                    case 1:
                        spawnposition = new Vector3(shot4.transform.position.x, shot4.transform.position.y, shot4.transform.position.z);
                        vulcNo = 0;
                        break;
                }
                vulcanCooldown = 0.025f;
                vulcanAmmo--;
                Vector3 vulcanRot = transform.rotation.eulerAngles;
                vulcanRot = new Vector3(vulcanRot.x + 90, vulcanRot.y, vulcanRot.z);
                GameObject instantiatedProjectile = Instantiate(vulcanProj, spawnposition, Quaternion.Euler(vulcanRot)) as GameObject;
                instantiatedProjectile.GetComponent<Rigidbody>().AddForce(rb.velocity * rb.velocity.magnitude);
            }
        }
        else {
            if (vulcanAmmo <= vulcanMax)
            {
                vulcanAmmo += 0.1f;
            }
        }
        ammo.size = vulcanAmmo / vulcanMax;
        cooldown.size = 1 - (shotCooldown / gunCooldown);
        pointsDisplay.text = "Points: " + points;
        if (vulcanAmmo <= vulcanMax / 5) {
            ColorBlock cb = ammo.colors;
            cb.normalColor = Color.red;
            ammo.colors = cb;
        }
        else if (vulcanAmmo <= vulcanMax / 2){
            ColorBlock cb = ammo.colors;
            cb.normalColor = Color.yellow;
            ammo.colors = cb;
        }
        else {
            ColorBlock cb = ammo.colors;
            cb.normalColor = Color.white;
            ammo.colors = cb;
        }
        if (1 - shotCooldown <= gunCooldown) {
            ColorBlock cb = cooldown.colors;
            cb.normalColor = Color.red;
            cooldown.colors = cb;
        }
        else {
            ColorBlock cb = cooldown.colors;
            cb.normalColor = Color.white;
            cooldown.colors = cb;
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
        cannon_k.transform.rotation = cameraPitch.transform.rotation;
        cannon_k.transform.Rotate(-90, 0, 0);
        shot3.transform.rotation = originPoint.transform.rotation;
        shot4.transform.rotation = originPoint.transform.rotation;

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

        //Particles
        if(Input.GetAxis("Vertical") != 0 && Input.GetAxis("Brake") == 0) {
            afterburner1.Play();
            afterburner2.Play();
            afterburner3.Play();
            afterburner4.Play();
        }
        else {
            afterburner1.Stop();
            afterburner2.Stop();
            afterburner3.Stop();
            afterburner4.Stop();
        }
    }

}