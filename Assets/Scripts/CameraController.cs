using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // offset is the difference between the camera's position and the player's position
        offset = transform.position - player.transform.position; 

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // camera's position is set to the player's position plus the offset
        transform.position = player.transform.position + offset; 
    }
}
