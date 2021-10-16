using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;  // is this wheel attached to motor?
    public bool steering;   // does this wheel apply steer angle?
}

public class CarController : MonoBehaviour {

    public float maxSpeed;
    public Rigidbody rb;
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float trust;
    public float fowardFriction;
    public float sidwaysFriction;
    public float defaultFowardFriction;
    public float defaultSidwaysFriction;
    public WheelFrictionCurve curve;
    public float breakTorque;

    private void Start()
    {
        
    }

    /// <summary>
    /// metodo usado para orientar e posicionar as rodas
    /// </summary>
    /// <param name="collider"></param>
    private void ApllyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        //escolhe o primeiro modelo que seja child do wheel colider
        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        //adicion 90 no z das rodas, para estarem verticais
        rotation = rotation * Quaternion.Euler(new Vector3(0, 0, 90));
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    /// <summary>
    /// metodo usado para verificar se é usado o turbo
    /// </summary>
    private void Turbo()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        //caso o shift seja carregado usa-se o nitro
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(rb.velocity * trust);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            rb.AddForce(rb.velocity * -trust * 3);
        }
    }

    /// <summary>
    /// metodo usado para travar o carro e fazer drift
    /// </summary>
    /// <param name="axleInfo"></param>
    private void DriftAndBreak(AxleInfo axleInfo)
    {
        //verifica se tou a travar ou não
        if (Input.GetKey(KeyCode.Space))
        {
            axleInfo.rightWheel.brakeTorque = axleInfo.leftWheel.brakeTorque = breakTorque;

            //muda define as characteristicas das para dift
            curve = axleInfo.rightWheel.forwardFriction;
            curve.asymptoteValue = fowardFriction;
            axleInfo.rightWheel.forwardFriction = curve;
            curve.asymptoteValue = sidwaysFriction;
            axleInfo.rightWheel.sidewaysFriction = curve;

            //faz o mesmo mas para a outra roda
            curve = axleInfo.leftWheel.forwardFriction;
            curve.asymptoteValue = fowardFriction;
            axleInfo.leftWheel.forwardFriction = curve;
            curve.asymptoteValue = sidwaysFriction;
            axleInfo.leftWheel.sidewaysFriction = curve;

        }
        else
        {
            axleInfo.rightWheel.brakeTorque = axleInfo.leftWheel.brakeTorque = 0f;

            //muda define as characteristicas das rodas para default
            curve = axleInfo.rightWheel.forwardFriction;
            curve.asymptoteValue = defaultFowardFriction;
            axleInfo.rightWheel.forwardFriction = curve;
            curve.asymptoteValue = defaultSidwaysFriction;
            axleInfo.rightWheel.sidewaysFriction = curve;

            //faz o mesmo mas para a outra roda
            curve = axleInfo.leftWheel.forwardFriction;
            curve.asymptoteValue = defaultFowardFriction;
            axleInfo.leftWheel.forwardFriction = curve;
            curve.asymptoteValue = defaultSidwaysFriction;
            axleInfo.leftWheel.sidewaysFriction = curve;

        }
    }

    private void Update()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.motor)
            {
                DriftAndBreak(axleInfo);
            }

            //verifica se tou a travar ou não
            if (Input.GetKey(KeyCode.LeftControl))
            {
                axleInfo.rightWheel.brakeTorque = axleInfo.leftWheel.brakeTorque = breakTorque;
            }
            else if (!Input.GetKey(KeyCode.Space)) { axleInfo.rightWheel.brakeTorque = axleInfo.leftWheel.brakeTorque = 0f; }
        }
    }

    /// <summary>
    /// metodo chamado por cada frame 
    /// </summary>
    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        //verifica o que cada par de rodas faz e faz essa ação
        //ex: se a roda for true como "motor" significa que ta liga ao motor e sera essas rodas que irão dar impulso
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {

                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            ApllyLocalPositionToVisuals(axleInfo.leftWheel);
            ApllyLocalPositionToVisuals(axleInfo.rightWheel);
        }

        Turbo();
    }
} 