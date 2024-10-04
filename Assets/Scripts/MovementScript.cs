using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Android;

public class MovementScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float acceleration;
    public Vector3 direction;

    public int jumpHeight;
    public bool grounded;

    public CameraRotation cameraRotation;

    public int baseMaxSpeed;

    public const int maxWeight = 100;

    public float weight;
    public float maxSpeed => CalculateMaxSpeed();

    public float CalculateMaxSpeed()
    {
        return baseMaxSpeed * ((maxWeight - weight) / maxWeight);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        Debug.DrawRay(player.transform.position, Vector3.down, Color.red);
        if (Physics.Raycast(player.transform.position, Vector3.down, 1.1f)) 
        {
            grounded = true;
        } 
        else
        {
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) 
        {
            rb.AddForce(0,jumpHeight * 10,0);
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * cameraRotation.sensitivity);
    }

   void FixedUpdate()
   {
        Vector3 newVelocity = rb.velocity;
        newVelocity += new Vector3(direction.x * acceleration, 0, direction.z * acceleration) * Time.fixedDeltaTime;

        Vector2 xyVelocity = new Vector3(newVelocity.x, newVelocity.z);

        if (xyVelocity.magnitude > maxSpeed) 
        { 
            xyVelocity.Normalize();
            xyVelocity *= maxSpeed;
        }

        newVelocity = new Vector3(xyVelocity.x, newVelocity.y, xyVelocity.y);

        rb.velocity = newVelocity;
   }


}
