using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private string stage_name;
    // Start is called before the first frame update
    public void Start()
    {
        audioSource.Play();
    }
    public void Play()
    {   
        audioSource.Stop();
        SceneManager.LoadScene(stage_name);
    }
}
