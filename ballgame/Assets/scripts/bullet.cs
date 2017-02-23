using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

    public int speed;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
        rb.AddForce(direction * speed);
        Destroy(gameObject, 5 - (speed / 1000));
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
