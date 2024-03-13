using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string stage_name;
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene(stage_name);
    }
}
