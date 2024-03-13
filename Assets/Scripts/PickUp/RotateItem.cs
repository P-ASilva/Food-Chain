using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    public float speed = 10.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (15, 30, 45) * speed * Time.deltaTime);
    }
}
