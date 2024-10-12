using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Android;

public class MovementScript : MonoBehaviour
{
    [Header("Player Physics")]
    public GameObject player;
    public Rigidbody rb;
    public float acceleration;
    public Vector3 direction;
    public int jumpHeight;
    public bool grounded;

    public CameraRotation cameraRotation;

    [Header("Movement")]
    public int baseMaxSpeed;
    public const int maxWeight = 100;
    public float weight;

    [Header("Not Magic Numbers")]
    public float groundCollisionRadius = 0.3f;
    public float groundCollisionDistance = 0.1f;
    public float maxSpeed => CalculateMaxSpeed();

    public LayerMask groundedMask;

    private Vector3 gizmosPosition;

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
        direction = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
        Debug.DrawRay(player.transform.position, Vector3.down, Color.red);
        if (Physics.SphereCast(player.transform.position, groundCollisionRadius, Vector3.down, out RaycastHit hit, groundCollisionDistance, groundedMask))
        {
            gizmosPosition = hit.point;
            Gizmos.color = Color.magenta;
            grounded = true;
        } 
        else
        {
            Gizmos.color = Color.red;
            gizmosPosition = player.transform.position + Vector3.down * groundCollisionDistance;
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
        if (grounded)      
        {
            GroundedMovement(); // later use?
        }
        else
        {
            AirMovement(); // later use?
        }
   }

    void GroundedMovement()
    {
        Vector2 xzVelocity = new Vector3(rb.velocity.x, rb.velocity.z);
        float yVelo = rb.velocity.y;



        if (direction != Vector3.zero)
        {
            xzVelocity += new Vector2(direction.x * acceleration, direction.z * acceleration) * Time.fixedDeltaTime;
        }
        else
        {
            xzVelocity *= 0.7f;
        }

        if (xzVelocity.magnitude > maxSpeed)
        {
            xzVelocity.Normalize();
            xzVelocity *= maxSpeed;
        }

        Vector3 newVelocity = new Vector3(xzVelocity.x, yVelo, xzVelocity.y);

        rb.velocity = newVelocity;
    }

    void AirMovement()
    {
        Vector2 xzVelocity = new Vector3(rb.velocity.x, rb.velocity.z);
        float yVelo = rb.velocity.y;



        if (direction != Vector3.zero)
        {
            xzVelocity += new Vector2(direction.x * acceleration, direction.z * acceleration) * Time.fixedDeltaTime;
        }
        else
        {
            xzVelocity *= 0.7f;
        }

        if (xzVelocity.magnitude > maxSpeed)
        {
            xzVelocity.Normalize();
            xzVelocity *= maxSpeed;
        }

        Vector3 newVelocity = new Vector3(xzVelocity.x, yVelo, xzVelocity.y);

        rb.velocity = newVelocity;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(gizmosPosition, groundCollisionRadius);
    }
}
