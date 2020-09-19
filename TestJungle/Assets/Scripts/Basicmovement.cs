using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basicmovement : MonoBehaviour
{
	public float speed = 5.0f;
    public float baseSpeed = 5.0f;
    public float speedMultiplier = 1.2f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
    public float rotSpeed = 2f ; 
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
    private Rigidbody rb;
    public LayerMask groundMask;

    Vector3 velocityChange = new Vector3(0,0,0);
    Vector3 velocity = new Vector3(0,0,0);
    bool jump = false;

    public GameObject cam;
	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
	    rb.freezeRotation = true;
	    rb.useGravity = false;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = baseSpeed * speedMultiplier;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = baseSpeed;
        }
        
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed;
        Vector3 posToLook = GetCameraTurn() * inputVector;

        velocity = rb.velocity;
        velocityChange = posToLook - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        if (inputVector != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(posToLook);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSpeed);
        }

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        } 
    }
    void FixedUpdate()
    {
        if (grounded)
        {
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            if (canJump && jump)
            {
                rb.velocity = new Vector3 (velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                jump = false;
            }
        }
    
        rb.AddForce(new Vector3 (0, -gravity * rb.mass, 0));
        grounded = false;
    }
   
    private Quaternion GetCameraTurn()
    {
        return Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up).normalized;
    }

	void OnCollisionStay (Collision collision) {
        if ((groundMask.value & 1<<collision.gameObject.layer) != 0)
	        grounded = true;    
	}

	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}

