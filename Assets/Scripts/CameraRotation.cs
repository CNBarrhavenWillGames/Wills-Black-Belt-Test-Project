using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public int sensitivity = 1;
    [SerializeField] private float rotation;

    private int invert = 1;
    // Update is called once per frame
    void Update() // Handles the Rotation of the Camera
    {
        if (PlayerPrefs.GetInt("InvertY") == 1)
        {
            invert = -1;
        }
        else
        {
            invert = 1;
        }

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

        rotation += Input.GetAxis("Mouse Y") * invert * Time.deltaTime * sensitivity;
        rotation = Mathf.Clamp(rotation, -80, 80);
        transform.localRotation = Quaternion.Euler(rotation, 0, 0);
    }
}
