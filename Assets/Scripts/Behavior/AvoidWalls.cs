using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidWalls : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public float speed = 0.5f;

    public float radius = 10.0f;
    // Update is called once per frame
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        anim = GetComponent<Animator>();
    }

    void Update() {
        Vector3 finalDirection = new Vector3(0, 0, 0);
        var colliders = Physics.OverlapSphere(transform.position, radius);
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
            } 
        }
        Debug.DrawRay(transform.position, finalDirection, Color.red);
        print(finalDirection);
        //Animate(finalDirection);
        rb.AddForce(Vector3.ClampMagnitude(finalDirection, speed));
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawLine(contact.point, Vector3.Normalize(transform.position - collision.transform.position) * speed, Color.white);
        }
        if (collision.gameObject.CompareTag("Wall_H") || collision.gameObject.CompareTag("Wall_V") || collision.gameObject.CompareTag("Goal"))
        {
            rb.velocity = Vector3.Normalize(transform.position - collision.transform.position) * speed*2;
        }
    } 
    
    void Animate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
            anim.SetInteger("Walk", 1);
        }
        else {
            anim.SetInteger("Walk", 0);
        }
    }
}
