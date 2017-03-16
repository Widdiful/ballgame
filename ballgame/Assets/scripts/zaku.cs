using UnityEngine;
using System.Collections;

public class zaku : MonoBehaviour {

    public GameObject projectile;
    public GameObject shootpos;
    public GameObject spawnzaku;
    public float speed;

    private GameObject[] players;
    private Quaternion spawnrotation;
    private Rigidbody rb;
    private Vector3 ballpos;
    private Vector3 movement;
    private Vector3 spawnposition;
    private float distance;
    private float shotCooldown;
    private int distanceMult;
    private int orbitDirection;

    // Use this for initialization
    void Start () {
        shotCooldown = Random.Range(1, 5);
        rb = GetComponent<Rigidbody>();
        distanceMult = Random.Range(1, 4);
        orbitDirection = Random.Range(0, 2);
        distance = Vector3.Distance(GameObject.Find("Mobile Pod Ball").transform.position, transform.position);
        if (distance <= 10) {
            Vector3 ballpos = GameObject.Find("Mobile Pod Ball").transform.position;
            spawnposition = new Vector3(ballpos.x + Random.Range(-50, 50), ballpos.y + Random.Range(-50, 50), ballpos.z + Random.Range(-10, 50));
            spawnrotation = new Quaternion();
            GameObject spawnedZaku = Instantiate(spawnzaku, spawnposition, spawnrotation) as GameObject;
            Destroy(gameObject);
        }
	}

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0) {
            ballpos = GameObject.Find("Mobile Pod Ball").transform.position;
            transform.LookAt(GameObject.Find("Mobile Pod Ball").transform);
        }
        distance = Vector3.Distance(ballpos, transform.position);
        if (distance > 5 * distanceMult)
        {
            movement = transform.rotation * Vector3.forward;
            GetComponent<Rigidbody>().velocity = movement * speed;
        }
        else
        {
            switch (orbitDirection)
            {
                case 0:
                    movement = transform.rotation * Vector3.right;
                    break;
                case 1:
                    movement = transform.rotation * Vector3.left;
                    break;
            }
            GetComponent<Rigidbody>().velocity = movement * speed;

        }
        if (distance < 3)
        {
            movement = transform.rotation * Vector3.back;
            GetComponent<Rigidbody>().velocity = movement * (speed / 3);
        }

        shotCooldown -= 0.01f;
        if (shotCooldown <= 0)
        {
            shotCooldown = 4 - (speed / 5);
            GameObject instantiatedProjectile = Instantiate(projectile, shootpos.transform.position, transform.rotation) as GameObject;
            instantiatedProjectile.GetComponent<Rigidbody>().AddForce(rb.velocity * rb.velocity.magnitude);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Rock")
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * speed;
        }
    }
}
