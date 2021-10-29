using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    //rigid body
    public Rigidbody sphereRB;
    public GameObject carModel;

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

    //drif
    private bool isDrifting;
    public float driftAngle;

    //brake
    public float breakForce; 
    private bool isBreaking;

    //turn variable
    float newRotation;

    //rotation variable
    Quaternion defaultRotation;

    //layerMasks
    public LayerMask groundLayer; 

    // Start is called before the first frame update
    void Start()
    {
        //stores the initial rotation of the modle
        defaultRotation = carModel.transform.rotation;

        //puts the sphere out of the hierarchy
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
        if (sphereRB.velocity.magnitude > 0f && Input.GetAxisRaw("Vertical") != 0)
        {
            newRotation = turningInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        }
        else if (sphereRB.velocity.magnitude > 0f)
        {
            newRotation = turningInput * turnSpeed * Time.deltaTime ;
        }
        transform.Rotate(0, newRotation, 0, Space.World);
        
        //checks if the raycast is hitting the gound 
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit,1f, groundLayer);

        //makes the car parallel to the gorund 
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;



        if (Input.GetKeyDown("space") && turningInput != 0)
        {
            carModel.transform.Rotate(0, turningInput * driftAngle , 0, Space.World);
            isDrifting = true;
        }
        else if(Input.GetKeyDown("space") && turningInput == 0)
        {
            isBreaking = true;
        }
        if (Input.GetKeyUp("space"))
        {
            carModel.transform.rotation = defaultRotation;
            isDrifting = false;
            isBreaking = false;
        }
    }


    //only used for physics updates
    private void FixedUpdate()
    {
        //updates de var speed so i can see the speed of the car in unity
        Speed = sphereRB.velocity.magnitude;

        //checks to see if the car is touching the ground
        if (isCarGrounded)
        {
            //checks if the player is moving and trying to break
            if (isDrifting)
            {
                //makes it easier to drift
                turnSpeed = 100f;
                sphereRB.drag = groundDrag / 3;
                sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
            }
            else if (isBreaking) 
            {
                //changes the turn speed and makes the car break
                turnSpeed = 80f;
                sphereRB.drag = groundDrag * 1.5f;
            }
            else
            {
                //moves the sphere and changes the drag
                turnSpeed = 80f;
                sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
                sphereRB.drag = groundDrag;
            }
        }
        else
        {
            //changes the drag
            sphereRB.drag = airDrag;
            
            //makes the car fall 
            sphereRB.AddForce(transform.up * -9.8f);
        }
    }
}
