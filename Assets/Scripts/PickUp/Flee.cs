using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float speed = 0.5f;
    public float radius = 10.0f;
    private Vector3 offset;
    // Update is called once per frame
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
    }

    void Update() {
        Vector3 finalDirection = new Vector3(0, 0, 0);
        var colliders = Physics.OverlapSphere(transform.position, radius);
        float step = speed * Time.deltaTime;
        foreach(var collider in colliders) {
            //Debug.DrawLine(contact.point, Vector3.Normalize(transform.position - collision.transform.position) * speed, Color.white);
            if (collider.gameObject.CompareTag("Wall_V"))
            {
                Vector3 direction = Vector3.Normalize(transform.position - collider.transform.position) * speed;
                direction.z = 0;
                finalDirection += direction;
            } else if (collider.gameObject.CompareTag("Wall_H"))
            {
                Vector3 direction = Vector3.Normalize(transform.position - collider.transform.position) * speed;
                direction.x = 0;
                finalDirection += direction;
            } else if (collider.gameObject.CompareTag("Player"))
            {
                finalDirection += Vector3.Normalize(transform.position - collider.transform.position) * speed;
            }  
        }
        Debug.DrawRay(transform.position, finalDirection, Color.red);
        print(finalDirection);
        rb.AddForce(Vector3.ClampMagnitude(finalDirection, speed));
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawLine(contact.point, Vector3.Normalize(transform.position - collision.transform.position) * speed, Color.white);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            // repel from wall
            rb.velocity = Vector3.Normalize(transform.position - collision.transform.position) * speed;
        }
    }
}
