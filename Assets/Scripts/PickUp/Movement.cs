using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float speed = 0.5f;
    public float radius = 5.0f;
    private Vector3 offset;
    // Update is called once per frame
        void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            var step =  speed * Time.deltaTime;
            var v = Vector3.MoveTowards(transform.position, player.transform.position, -step);
            rb.velocity = new Vector3(v.x, 0, v.z);
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            // reflect off the wall
        }

    }
}
