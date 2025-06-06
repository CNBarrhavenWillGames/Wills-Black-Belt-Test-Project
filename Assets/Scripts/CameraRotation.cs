using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// camera x pos is usually 0.921
public class CameraRotation : MonoBehaviour
{
    public int sensitivity = 1;
    [SerializeField] private float rotation;

    private int invert = 1;

    private RaycastHit hit;

    public Camera Camera;

    public Transform cameraTransform;

    private Vector3 cameraOffset;

    public bool useExperimentalCamera;
    private void Start()
    {
        cameraOffset = cameraTransform.localPosition; // Sets the normal distance to whatever it is at the beginning, when it's not colliding with anything.
    }

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
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            sensitivity += 5;
        }

        rotation += Input.GetAxis("Mouse Y") * invert * Time.deltaTime * sensitivity;
        rotation = Mathf.Clamp(rotation, -84, 84);
        transform.localRotation = Quaternion.Euler(rotation, 0, 0);

        if (useExperimentalCamera)
        {
            if (Physics.Linecast(transform.position, transform.position + transform.rotation * cameraOffset, out hit)) // if something is colliding with the line from camera target to camera,
            {
                cameraTransform.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point)); // move the camera to that collision point
            }
            else
            {
                cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, cameraOffset, Time.deltaTime); // Sets the camera back to its original position
            }

            Debug.DrawLine(transform.position, transform.position + transform.rotation * cameraOffset);
        }

    }
}
