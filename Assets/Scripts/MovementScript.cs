using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Android;

public class MovementScript : MonoBehaviour
{
    [Header("Player Physics")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration;
    [SerializeField] private Vector3 direction;
    [SerializeField] private int jumpHeight;
    [SerializeField] private bool grounded;

    [SerializeField] private CameraRotation cameraRotation;

    [Header("Movement")]
    [SerializeField] private int baseMaxSpeed;
    public const int maxWeight = 100;
    public float weight;

    [Header("Not Magic Numbers")]
    [SerializeField] private float groundCollisionRadius = 0.4f;
    [SerializeField] private float groundCollisionDistance = 0.9f;
    [SerializeField] private float jumpBuffer = 0.5f;
    [SerializeField] private float coyoteTime = 0.5f;
    [SerializeField] private float jumpCooldown = 0.2f;
    private float maxSpeed => CalculateMaxSpeed();

    [SerializeField] private LayerMask groundedMask;

    private Vector3 gizmosPosition;

    private int invert;

    private float LastJumpInputTime;
    private float LastJumpSuccessTime;
    private float LastGroundedTime;



    /// <summary>
    /// This function calculates the new max speed of the player based on weight.
    /// </summary>
    /// <returns>New Max Speed</returns>
    private float CalculateMaxSpeed()
    {
        return baseMaxSpeed * ((maxWeight - weight) / maxWeight);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        LastJumpInputTime = float.NegativeInfinity;
    }

    // Update is called once per frame
    public void Update()
    {
        if (PlayerPrefs.GetInt("InvertX") == 1)
        {
            invert = -1;
        }
        else
        {
            invert = 1;
        }

        direction = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal"); // Get Input

        Debug.DrawRay(player.transform.position, Vector3.down, Color.red);

        if (Physics.SphereCast(player.transform.position, groundCollisionRadius, Vector3.down, out RaycastHit hit, groundCollisionDistance, groundedMask)
            && Time.time - LastJumpSuccessTime > jumpCooldown) // Ground Collision Detection
        {
            gizmosPosition = hit.point;
            Gizmos.color = Color.magenta;
            grounded = true;
            LastGroundedTime = Time.time;
        } 
        else
        {
            Gizmos.color = Color.red;
            gizmosPosition = player.transform.position + Vector3.down * groundCollisionDistance;
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            LastJumpInputTime = Time.time;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * invert, 0) * Time.deltaTime * cameraRotation.sensitivity); // Rotates Player with Mouse Input
    }

   private void FixedUpdate()
   {

        if (Time.time - LastGroundedTime <= coyoteTime)   // Both functions perform the same actions, but it might be handy to differentiate them later.   
        {
            GroundedMovement();

            if (Time.time - LastJumpInputTime <= jumpBuffer)
            {
                print(Time.time - LastJumpInputTime + "/" + jumpBuffer);
                LastJumpInputTime = float.NegativeInfinity;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);


                rb.AddForce(0, jumpHeight * 10, 0);
                LastJumpSuccessTime = Time.time;
            }
        }
        else
        {
            AirMovement();
        }
   }
    /// <summary>
    /// This function handles player movement on the ground.
    /// </summary>
    private void GroundedMovement()
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

    /// <summary>
    /// This function handles player movement in the air.
    /// </summary>
    private void AirMovement()
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
