using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private GameObject hermesBootsObject;

    [Header("Player Physics")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration;
    [SerializeField] private Vector3 direction;
    [SerializeField] private int jumpHeight;
    //[SerializeField] private bool grounded;

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

    public GameObject minimap;

    public GameObject tutorialSpawn;

    [SerializeField] private float DagansDeltaLastGroundedTime;

    private Vector3 startingPosition;

    [SerializeField] private Animator characterAnimator;

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

        startingPosition = gameObject.transform.position;

        if (DataStorage.saveData.day == 1 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameObject.transform.position = tutorialSpawn.transform.position + (Vector3.up * 2);
        }

        if (DataStorage.saveData.hermesBoots == true)
        {
            baseMaxSpeed = 6;
            hermesBootsObject.SetActive(true);
        }

        if (DataStorage.saveData.extraHealth == true)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1, 1);
        }

        if (DataStorage.saveData.map == true)
        {
            minimap.SetActive(true);
        }

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

        characterAnimator.SetFloat("MovementX", direction.x);
        characterAnimator.SetFloat("MovementZ", direction.y);

        Debug.DrawRay(player.transform.position, Vector3.down, Color.red);

        if (Physics.SphereCast(player.transform.position, groundCollisionRadius, Vector3.down, out RaycastHit hit, groundCollisionDistance, groundedMask)
            && Time.time - LastJumpSuccessTime > jumpCooldown) // Ground Collision Detection
        {
            gizmosPosition = hit.point;
            Gizmos.color = Color.magenta;
            //grounded = true;
            LastGroundedTime = Time.time;
        } 
        else
        {
            Gizmos.color = Color.red;
            gizmosPosition = player.transform.position + Vector3.down * groundCollisionDistance;
            //grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            LastJumpInputTime = Time.time;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * invert, 0) * Time.deltaTime * cameraRotation.sensitivity); // Rotates Player with Mouse Input

        DagansDeltaLastGroundedTime = Time.time - LastGroundedTime;
    }

   private void FixedUpdate()
   {

        if (Time.time - LastGroundedTime <= coyoteTime)   // Both functions perform the same actions, but it might be handy to differentiate them later.   
        {
            GroundedMovement();

            if (Time.time - LastJumpInputTime <= jumpBuffer)
            {
                LastJumpInputTime = float.NegativeInfinity;
                //HI WILL we fixed it
                // you know how you set LastJumpInputTime to NegativeInfinity to 'consume' the jump input
                // since NegativeInfinity seconds is a long time ago, so we definetly don't want to jump
                // you also have to set LastGroundedTime to NegativeInfinity to 'consume' the 'grounded'
                // since if we were last on the ground infinite time ago that's a really long time and coyote can't run/jump
                // we don't need to do a similar LastJumpSuccessTime reset, since the = Time.time does that?
                // idk seems to work probably.
                // -Dagan
                // Amanda Was Here
                LastGroundedTime = float.NegativeInfinity;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);


                rb.AddForce(0, jumpHeight * 10, 0);
                LastJumpSuccessTime = Time.time;
            }
        }
        else
        {
            GroundedMovement(); // AirMovement() if that ever becomes relevant
        }

        if (transform.position.y <= -5)
        {
            transform.position = startingPosition;
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
