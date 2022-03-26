using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;

public class CarControl : MonoBehaviour
{
    //rigid body
    public Rigidbody sphereRB;
    public GameObject carModel;

    //inputs
    private float moveInput;
    private float turningInput;

    public ParticleSystem driftParticlesLeft;
    public ParticleSystem driftParticlesRight;

    //raycast
    private bool isCarGrounded;
    private bool isCarOutOfRoad;

    public float Speed;
    public float ReverseSpeed = ActiveCar.FowardSpeed * 0.40f;

    [Header("Drift/Turning")]
    //drif
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
    public LayerMask outOfRoadLayer;

    public static bool canMove = true;

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start()
    {
        wrongSideDrift = ActiveCar.TurnSpeed / 2.5f;

        canMove = false;

        ReverseSpeed = ActiveCar.FowardSpeed * 0.75f;

        //puts the sphere out of the hierarchy
        sphereRB.transform.parent = null;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (anim == null)
        {
            Debug.Log("animator was null");
            anim = GameObject.Find("CarRoot/CarModel/Car").GetComponent<Animator>();
        }

        if (driftParticlesLeft == null )
        {
            driftParticlesLeft = GameObject.Find("CarRoot/CarModel/Car/particulas left").GetComponent<ParticleSystem>();
        }

        if (driftParticlesRight == null)
        {
            driftParticlesRight = GameObject.Find("CarRoot/CarModel/Car/particulas right").GetComponent<ParticleSystem>();
        }

        //verifica se ja se pode mover
        if (canMove)
        {
            //gets and prepares the input
            moveInput = Input.GetAxisRaw("Vertical");

            if (moveInput > 0f)
            {
                moveInput *= ActiveCar.FowardSpeed;
            }
            else if (moveInput < 0f)
            {
                moveInput *= ReverseSpeed;
            }

            turningInput = Input.GetAxisRaw("Horizontal");
        }

        //sets the car position the same as the spheres
        transform.position = sphereRB.transform.position;

        //metodo para rodar o carro
        Turn();

        //checks if the raycast is hitting the gound 
        RaycastHit hit;
        RaycastHit hit2;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 2f, groundLayer);
        isCarOutOfRoad = Physics.Raycast(transform.position, -transform.up, out hit2, 2f, outOfRoadLayer);

        //makes the car parallel to the gorund 
        if (isCarGrounded)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        else if (isCarOutOfRoad)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit2.normal) * transform.rotation;
        }

        //checks to see if the car is touching the ground
        if (isCarGrounded || isCarOutOfRoad)
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
        if (Speed > 1f && !isDrifting)
        {
            //verifica se o jogador esta a andar para a frente ou não, caso não inverte as direções 
            newRotation = Input.GetAxisRaw("Vertical") < 0f ? turningInput * ActiveCar.TurnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical") : turningInput * ActiveCar.TurnSpeed * Time.deltaTime;

            //if (Input.GetAxisRaw("Vertical") < 0f)
            //{
            //    newRotation = turningInput * ActiveCar.TurnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
            //}
            //else
            //{
            //    newRotation = turningInput * ActiveCar.TurnSpeed * Time.deltaTime;
            //}
        }
        else if (Speed > 1f && isDrifting) //verifica se o jogador esta a andar e a fazer drift para contar os pontos
        {
            gameObject.GetComponent<nitro>().NitroPoints();

            //sees wich side is being and compares it with the side that is drifting
            if (turningInput != driftSide)
            {
                newRotation = turningInput * wrongSideDrift * Time.deltaTime;
            }
            else
            {
                newRotation = turningInput * ActiveCar.TurnSpeed * Time.deltaTime;
            }
        }
        else
        {
            newRotation = 0;
        }

        //rotates the car model
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    /// <summary>
    /// decides if the car should play the drift animation or not
    /// </summary>
    private void DriftAnimation()
    {
        //inicia as animações dependendo do lado em que o jogador esta a tentar virar
        if (turningInput > 0)
        {
            anim.SetBool("isRightDrifting", true);
            driftSide = 1;
            turningInput += 0.5f;
        }
        else if (turningInput < 0)
        {
            anim.SetBool("isLeftDrifting", true);
            driftSide = -1;
            turningInput -= 0.5f;
        }
    }


    /// <summary>
    /// metodo usado para verificar o que fazer quando o carro ta a travar
    /// </summary>
    private void StartBreaking()
    {
        if (Input.GetKeyDown("space") && turningInput != 0 && !isDrifting) //verifica se o jogador quer fazer drift ou não 
        {
            //sees if the car should be drifting or not
            DriftAnimation();
            driftParticlesRight.Play();
            driftParticlesLeft.Play();
            isDrifting = true;
        }
        else if (Input.GetKeyDown("space") && turningInput == 0) //verifica se o jogador estava a virar antes quando carregou no travão
        {
            anim.SetBool("isLeftDrifting",false);
            anim.SetBool("isRightDrifting",false);
            anim.SetBool("isBreaking",true);
            isBreaking = true;
            driftParticlesRight.Stop();
            driftParticlesLeft.Stop();
        }
        else if (!Input.GetKey("space")) //caso nao esteja a travar desativa as animações 
        {
            anim.SetBool("isLeftDrifting", false);
            anim.SetBool("isRightDrifting", false);
            anim.SetBool("isBreaking", false);
            isBreaking = false;
            isDrifting = false;
            driftParticlesRight.Stop();
            driftParticlesLeft.Stop();
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
            //caso a ultima ação tenha sido travar
            if (isBreaking)
            {
                //stops the breaking animation
                anim.SetBool("isBreaking", false);
                isBreaking = false;
            }

            //caso tenha sido fazer drift
            if (isDrifting)
            {
                driftParticlesRight.Stop();
                driftParticlesLeft.Stop();
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
        if (canMove)
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
                    ActiveCar.TurnSpeed = ActiveCar.DriftTurnAngle;
                    sphereRB.drag = ActiveCar.GroundDrag;
                    sphereRB.AddForce(transform.forward * (moveInput * 0.80f), ForceMode.Acceleration);
                }
                else if (isBreaking)
                {
                    //changes the turn speed and makes the car break
                    ActiveCar.TurnSpeed = ActiveCar.DefaultTurnAngle;
                    sphereRB.drag = ActiveCar.GroundDrag * 2f;
                }
                else
                {
                    //moves the sphere and changes the drag
                    ActiveCar.TurnSpeed = ActiveCar.DefaultTurnAngle;
                    sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
                    sphereRB.drag = ActiveCar.GroundDrag;
                }
            }
            else if (isCarOutOfRoad) //verifica se o carro esta fora da estrada e penaliza o jogador caso esteja
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
                    ActiveCar.TurnSpeed = ActiveCar.DriftTurnAngle;
                    sphereRB.drag = ActiveCar.GroundDrag;
                    sphereRB.AddForce(transform.forward * (moveInput * 0.70f), ForceMode.Acceleration);
                }
                else if (isBreaking)
                {
                    //changes the turn speed and makes the car break
                    ActiveCar.TurnSpeed = ActiveCar.DefaultTurnAngle;
                    sphereRB.drag = ActiveCar.GroundDrag * 2.3f;
                }
                else
                {
                    //moves the sphere and changes the drag
                    ActiveCar.TurnSpeed = ActiveCar.DefaultTurnAngle;
                    sphereRB.AddForce(transform.forward * (moveInput * 0.75f), ForceMode.Acceleration);
                    sphereRB.drag = ActiveCar.GroundDrag * 0.95f;
                }
            }
            else
            {
                //changes the drag
                sphereRB.drag = ActiveCar.AirDrag;

                //makes the car fall 
                sphereRB.AddForce(transform.up * -9.8f);
            }
        }
    }
}
