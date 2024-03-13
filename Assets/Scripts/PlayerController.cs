using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody rb; 
    private float movementX;
    private float movementY;
    public float speed = 10; 
    public int count = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    // Start is called before the first frame update
    void Start()
    {
     rb = GetComponent <Rigidbody>();
     winTextObject.SetActive(false);
     SetCountText();
    }

    void OnMove (InputValue movementValue)
   {
     Vector2 movementVector = movementValue.Get<Vector2>();
     movementX = movementVector.x; 
     movementY = movementVector.y; 
   }

    void SetCountText() 
   {
    countText.text =  "Score: " + count.ToString();
    if (count >= 2)
    {
    winTextObject.SetActive(true);
    }
   }

    private void FixedUpdate() 
   { 
    Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
    rb.AddForce(movement * speed);  
   }
    void OnCollisionEnter(Collision collision)
    {
      foreach (ContactPoint contact in collision.contacts)
      {
        Debug.DrawRay(contact.point, contact.normal, Color.white);
      }
      // var colliders = Physics.OverlapSphere(transform.position, 10f)
      // foreach(var collider in colliders) {
      //       Debug.Log($"{collider.gameObject.name} is nearby");
      // }
      if (collision.gameObject.CompareTag("PickUp"))
      {
        collision.gameObject.SetActive(false);
        count++;
        SetCountText();
      } else if (collision.gameObject.CompareTag("Seeker")) {
        collision.gameObject.SetActive(false);
        count--;
        SetCountText();
      }
    }
   
}

