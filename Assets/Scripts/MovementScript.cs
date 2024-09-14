using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class MovementScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float acceleration;
    public Vector3 direction;
    public float maxSpeed;


    public int jumpHeight;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Debug.DrawRay(player.transform.position, Vector3.down, Color.red);
        if (Physics.Raycast(player.transform.position, Vector3.down, 1.1f)) 
        {
            grounded = true;
            print("grounded");
        } 
        else
        {
            grounded = false;
            print("not grounded");
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) 
        {
            rb.AddForce(0,jumpHeight * 10,0);
        }
    }

   void FixedUpdate()
   {
        Vector3 newVelocity = rb.velocity;
        newVelocity += new Vector3(direction.x * acceleration, 0, direction.y * acceleration) * Time.fixedDeltaTime;

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
