using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{   
    private Rigidbody rb; 
    private float movementX;
    private float movementY;
    public float speed = 10; 
    public int count = 0;
    public float timeLeft = 120.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    // Start is called before the first frame update
    
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      winTextObject.SetActive(false);
      loseTextObject.SetActive(false);
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
    if (count >= 200)
    {
    winTextObject.SetActive(true);
    }
   }

    private void Update() 
   { 
    Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
    rb.AddForce(movement * speed);  
    // update
    timeLeft -= Time.deltaTime;
    if (timeLeft <= 0 && OutOfBounds()) // swap to or
    {
      loseTextObject.SetActive(true);
      StartCoroutine(WaitAndLoadMenu());
      SceneManager.LoadScene("Menu");
    }
   }
    void OnCollisionEnter(Collision collision)
    {
      foreach (ContactPoint contact in collision.contacts)
      {
        Debug.DrawRay(contact.point, contact.normal, Color.white);
      }
      if (collision.gameObject.CompareTag("PickUp"))
      {
        collision.gameObject.SetActive(false);
        count++;
        SetCountText();
      } else if (collision.gameObject.CompareTag("Seeker")) {
        loseTextObject.SetActive(true);
        StartCoroutine(WaitAndLoadMenu());
        SceneManager.LoadScene("Menu");
    
      } else if (collision.gameObject.CompareTag("Goal")) {
        // win text
        winTextObject.SetActive(true);
        StartCoroutine(WaitAndLoadMenu());
      }
    }
    IEnumerator WaitAndLoadMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }
    bool OutOfBounds()
    {
      if (transform.position.y < -70 || transform.position.y < 70 || transform.position.x < -70 || transform.position.x > 70 || transform.position.z < -70 || transform.position.z > 70)
      {
        return true;
      }
      return false;
    }
}

