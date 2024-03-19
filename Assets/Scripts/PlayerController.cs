using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{   
    private Rigidbody rb; 
    public AudioSource audioSource;
    public AudioSource pickupSound;
    public AudioSource loseSound;
    public AudioSource winSound;
    private float movementX;
    private float movementY;
    public float speed = 10; 
    public int count = 0;
    public float timeLeft = 120.0f;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    // Start is called before the first frame update
    
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      audioSource.Play();
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
   }
   void SetTimerText() {
    float minutes = Mathf.Floor(timeLeft / 60);
    float seconds = timeLeft%60;
    if (seconds <= 10) {
      timerText.text = "Time: " + minutes.ToString() + ":0" +Mathf.Floor(seconds);
    } else if (timeLeft > 0){
      timerText.text = "Time: " + minutes.ToString() + ":" + Mathf.Floor(seconds);
    } else {
      timerText.text = "Time: 0:00";
    }
   }

    private void Update() 
   { 
    Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
    rb.AddForce(movement * speed);  
    // update
    if (timeLeft > 0)
    {
      timeLeft -= Time.deltaTime;
    }
    SetTimerText();
    if (timeLeft <= 0 || OutOfBounds()) // swap to or
    {
      if (!winTextObject.activeSelf) {
        loseTextObject.SetActive(true);
        audioSource.Stop();
        loseSound.Play();
      }
      StartCoroutine(WaitAndLoadMenu(5,loseSound));
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
        pickupSound.Play();
        count++;
        SetCountText();
      } else if (collision.gameObject.CompareTag("Seeker") && !winTextObject.activeSelf) {
        loseTextObject.SetActive(true);
        audioSource.Stop();
        loseSound.Play();
        StartCoroutine(WaitAndLoadMenu(5,loseSound));
    
      } else if (collision.gameObject.CompareTag("Goal") && count >= 16 && !loseTextObject.activeSelf) {
        winTextObject.SetActive(true);
        audioSource.Stop();
        winSound.Play();
        StartCoroutine(WaitAndLoadMenu(5,winSound));
      }
    }
    IEnumerator WaitAndLoadMenu(int wait, AudioSource audio = null)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("Menu");
        audio.Stop();
    }
    bool OutOfBounds()
    {
      if (transform.position.y < -70 || transform.position.y > 70 || transform.position.x < -70 || transform.position.x > 70 || transform.position.z < -70 || transform.position.z > 70)
      {
        return true;
      }
      return false;
    }
}

