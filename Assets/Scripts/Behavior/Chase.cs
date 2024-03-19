using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject target;
    private Rigidbody rb;
    private Animator anim;
    public float speed = 0.5f;
    public float radius = 10.0f;
    private Vector3 offset;
    // Update is called once per frame
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
    }

    void Update() {
        Vector3 finalDirection = new Vector3(0, 0, 0);
        var colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(var collider in colliders) {
            //Debug.DrawLine(contact.point, Vector3.Normalize(transform.position - collision.transform.position) * speed, Color.white);
            if (collider.gameObject.Equals(target))
            {
                finalDirection += Vector3.Normalize(collider.transform.position - transform.position) * speed;
            }  
        }
        Debug.DrawRay(transform.position, finalDirection, Color.red);
        print(finalDirection);
        rb.AddForce(Vector3.ClampMagnitude(finalDirection, speed));
        //Animate(finalDirection);
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
