using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    //rigid body
    public Rigidbody sphereRB;

    //inputs
    private float moveInput;
    private float turningInput;

    //raycast
    private bool isCarGrounded;

    //speeds
    public float fowardSpeed;
    public float reverseSpeed;
    public float turnSpeed;
    public float groundDrag;
    public float airDrag;
    public float Speed;

    //layerMasks
    public LayerMask groundLayer; 

    // Start is called before the first frame update
    void Start()
    {
        sphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //gets and prepares the input
        moveInput = Input.GetAxisRaw("Vertical");
        moveInput *= moveInput > 0 ? fowardSpeed : reverseSpeed;
        turningInput = Input.GetAxisRaw("Horizontal");

        //sets the car position the same as the spheres
        transform.position = sphereRB.transform.position;

        //rotates the car
        float newRotation = turningInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);
        
        //checks if the raycast is hitting the gound 
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit,1f, groundLayer);

        //makes the car parallel to the gorund 
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        //changes the drag dependig on wheater or not the car is in the air
        if (isCarGrounded)
        {
            sphereRB.drag = groundDrag;
        }
        else
        {
            sphereRB.drag = airDrag;
        }
    }


    //only used for physics updates
    private void FixedUpdate()
    {
        Speed = sphereRB.velocity.magnitude;

        if (isCarGrounded)
        {
            //moves the sphere
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            //makes the car fall 
            sphereRB.AddForce(transform.up * -9.8f);
        }
    }
}
