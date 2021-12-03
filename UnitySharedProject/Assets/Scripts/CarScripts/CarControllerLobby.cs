using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerLobby : MonoBehaviour
{
    //rigid body
    public Rigidbody sphereRB;
    public GameObject carModel;

    //inputs
    private float moveInput;
    private float turningInput;

    private ParticleSystem driftParticles;

    //raycast
    private bool isCarGrounded;

    [Header("Speeds")]
    //speeds
    public float fowardSpeed;
    public float reverseSpeed;
    public float turnSpeed;
    public float groundDrag;
    public float airDrag;
    public float Speed;

    //turn variable
    float newRotation;

    [Header("Animation")]
    //variable that will control the car animations
    public Animator anim;

    [Header("Masks")]
    //layerMasks
    public LayerMask groundLayer;

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start()
    {
        //encontra o componente no car model que seja um sistema de particulas
        driftParticles = carModel.GetComponent<ParticleSystem>();

        //puts the sphere out of the hierarchy
        sphereRB.transform.parent = null;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //gets and prepares the input
        moveInput = Input.GetAxisRaw("Vertical");
        moveInput *= moveInput > 0 ? fowardSpeed : reverseSpeed;
        turningInput = Input.GetAxisRaw("Horizontal");

        //sets the car position the same as the spheres
        transform.position = sphereRB.transform.position;

        //metodo para rodar o carro
        Turn();

        //checks if the raycast is hitting the gound 
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 4f, groundLayer);

        //makes the car parallel to the gorund 
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
    }

    /// <summary>
    /// metod that turns the car
    /// </summary>
    private void Turn()
    {
        //normal rotation of the car
        if (sphereRB.velocity.magnitude > 1f && Input.GetAxisRaw("Vertical") != 0)
        {
            newRotation = turningInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        }

        //rotates the car model
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    /// <summary>
    /// only used for physics updates
    /// </summary>
    private void FixedUpdate()
    {
        //updates de var speed so i can see the speed of the car in unity
        Speed = sphereRB.velocity.magnitude;

        //checks to see if the car is touching the ground
        if (isCarGrounded)
        {
            
            
            //checks to see if it should play the is moving animation based on the cars speed
            if (sphereRB.velocity.magnitude > 2f)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            //moves the sphere and changes the drag
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
            sphereRB.drag = groundDrag;
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
