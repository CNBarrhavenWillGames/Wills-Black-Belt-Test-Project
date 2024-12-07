using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public int sensitivity = 1;
    [SerializeField] private float rotation;

    // Update is called once per frame
    void Update() // Handles the Rotation of the Camera
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            sensitivity -= 5;
            print("Sensitivity:" + sensitivity);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            sensitivity += 5;
            print("Sensitivity:" + sensitivity);
        }

        rotation += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        rotation = Mathf.Clamp(rotation, -80, 80);
        transform.localRotation = Quaternion.Euler(rotation, 0, 0);
    }
}
