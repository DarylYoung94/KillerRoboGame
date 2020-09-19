using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basicmovement : MonoBehaviour
{
    public float movespeed;
    public Rigidbody rigid;
    public GameObject cam;
    public float jumpForce;
    public float gravity ;
    public bool flight = true;
    public float verticalVelocity;
    public float flightGravity;
    public float rotSpeed = 2f ; 
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        movespeed = 4f;
        jumpForce = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerActive)
        {
            if (flight)
            {
                gravity = flightGravity * Time.deltaTime;
            }
            else
            {
                gravity = 0;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                  
                    rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                movespeed = movespeed + 4f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                movespeed = movespeed - 4f;
            }

            transform.Rotate(new Vector3(0, 0, 0));
            Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal") * movespeed, 0, Input.GetAxisRaw("Vertical") * movespeed);
            verticalVelocity = gravity;
            Vector3 posToLook = GetCameraTurn() * inputVector;
            rigid.velocity =  posToLook - new Vector3(0, verticalVelocity, 0);
            
            if (inputVector != Vector3.zero)
            {
                Debug.DrawRay(transform.position + posToLook, Vector3.up, Color.cyan);
                Quaternion rotation = Quaternion.LookRotation(posToLook);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSpeed);
            }           
        }
        else
        {
            rigid.velocity = new Vector3(0, 0, 0);
        }
    }
   
    private Quaternion GetCameraTurn()
    {
        return Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            flight = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            flight = true;
        }
    }
    void CamLock()
    {
        
    }

}

