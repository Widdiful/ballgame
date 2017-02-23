using UnityEngine;
using System.Collections;

public class BallOrigin : MonoBehaviour
{

    public GameObject target;
    public int rotationspeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
    }

    void FixedUpdate()
    {
        float rotateX = Input.GetAxis("Horizontal");

        transform.Rotate(0 * rotationspeed, rotateX * rotationspeed, 0);
    }
}
