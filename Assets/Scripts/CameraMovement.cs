using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera Camera;
    public Vector3 cameraPosition;
    public Quaternion cameraRotation;

    public GameObject player;
    public GameObject cameraController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //cameraPosition = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 4);
        //Camera.transform.position = cameraPosition;
        transform.LookAt(cameraController.transform);
    }
}
