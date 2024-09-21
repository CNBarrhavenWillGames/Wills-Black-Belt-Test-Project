using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public int sensitivity = 1;
    public float rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        rotation = Mathf.Clamp(rotation, -80, 80);
        transform.localRotation = Quaternion.Euler(rotation, 0, 0);
    }
}
