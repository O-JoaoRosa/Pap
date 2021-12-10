using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
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

    [Header("Drift/Turning")]
    //drif
    public float defaultTurnAngle;
    public float DriftTurnAngle;
    public float wrongSideDrift;
    private bool isDrifting;
    private float driftSide;
    private nitro test;

    [Header("Break")]
    //brake
    public float breakForce; 
    private bool isBreaking;
    
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
        wrongSideDrift = turnSpeed / 4;

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
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit,1f, groundLayer);

        //makes the car parallel to the gorund 
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        //checks to see if the car is touching the ground
        if (isCarGrounded)
        {
            StartBreaking();
        }
    }

    /// <summary>
    /// metod that turns the car
    /// </summary>
    private void Turn()
    {
        //normal rotation of the car
        if (sphereRB.velocity.magnitude > 1f && Input.GetAxisRaw("Vertical") != 0 && !isDrifting)
        {
            newRotation = turningInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        }
        else if (sphereRB.velocity.magnitude > 0f && isDrifting)
        {
            gameObject.GetComponent<nitro>().NitroPoints();

            //sees wich side is being and compares it with the side that is drifting
            if (turningInput != driftSide)
            {
                newRotation = turningInput * wrongSideDrift * Time.deltaTime;
            }
            else
            {
                newRotation = turningInput * turnSpeed * Time.deltaTime;
            }
        }

        //rotates the car model
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    /// <summary>
    /// decides if the car should play the drift animation or not
    /// </summary>
    private void DriftAnimation()
    {
        if (turningInput > 0)
        {
            anim.SetBool("isRightDrifting", true);
            driftSide = 1;
        }
        else if (turningInput < 0)
        {
            anim.SetBool("isLeftDrifting", true);
            driftSide = -1;
        }
    }


    /// <summary>
    /// metodo usado para verificar o que fazer quando o carro ta a travar
    /// </summary>
    private void StartBreaking()
    {
        if (Input.GetKeyDown("space") && turningInput != 0 && !isDrifting)
        {
            //sees if the car should be drifting or not
            DriftAnimation();
            driftParticles.Play();
            isDrifting = true;
        }
        else if (Input.GetKeyDown("space") && turningInput == 0)
        {
            anim.SetBool("isLeftDrifting",false);
            anim.SetBool("isRightDrifting",false);
            anim.SetBool("isBreaking",true);
            isBreaking = true;
        }
        else if (!Input.GetKey("space"))
        {
            anim.SetBool("isLeftDrifting", false);
            anim.SetBool("isRightDrifting", false);
            anim.SetBool("isBreaking", false);
            isBreaking = false;
            isDrifting = false;
        }
    }

    /// <summary>
    /// metodo usado para parar de travar
    /// </summary>
    private void StopBreaking()
    {

        //devolve os valores default as bools
        if (!Input.GetKey("space") && (isDrifting || isBreaking))
        {
            //caso a ultima a��o tenha cido travar
            if (isBreaking)
            {
                //stops the breaking animation
                anim.SetBool("isBreaking", false);
                isBreaking = false;
            }

            //caso tenha cido fazer drift
            if (isDrifting)
            {
                driftParticles.Stop();
                isDrifting = false;

                //stops the drifting animation
                anim.SetBool("isLeftDrifting", false);
                anim.SetBool("isRightDrifting", false);
            }
        }
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
            StopBreaking();
            
            //checks to see if it should play the is moving animation based on the cars speed
            if (sphereRB.velocity.magnitude > 2f)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            //checks if the player is moving and trying to break
            if (isDrifting)
            {
                //makes it easier to drift
                turnSpeed = DriftTurnAngle;
                sphereRB.drag = groundDrag / 3;
                sphereRB.AddForce(transform.forward * moveInput / 3, ForceMode.Acceleration);
            }
            else if (isBreaking) 
            {
                //changes the turn speed and makes the car break
                turnSpeed = defaultTurnAngle;
                sphereRB.drag = groundDrag * 1.5f;
            }
            else
            {
                //moves the sphere and changes the drag
                turnSpeed = defaultTurnAngle;
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