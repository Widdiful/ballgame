using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

    public int speed;
    public float destroyTime;
    public GameObject explosionEffect;

    private Rigidbody rb;
    private GameObject owner;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        owner = FindClosestActor();
        Vector3 direction = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
        rb.AddForce(direction * speed);
        Destroy(gameObject, destroyTime);
    }
	
	// Update is called once per frame
	void OnCollisionEnter (Collision collision) {
        if (collision.collider.tag == "Actor") {
            Destroy(collision.gameObject);
            PlayerController pcon = GameObject.Find("Mobile Pod Ball").GetComponent<PlayerController>();
            pcon.points += 1;
            rb.AddExplosionForce(100, transform.position, 5);
            GameObject explosion = Instantiate(explosionEffect, collision.transform.position, collision.transform.rotation) as GameObject;
            Destroy(explosion, 2);
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    GameObject FindClosestActor()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Actor");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
