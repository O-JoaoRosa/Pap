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

    public void Start()
    {
        
    }

    /// <summary>
    /// metodo usado para orientar e posicionar as rodas
    /// </summary>
    /// <param name="collider"></param>
    public void ApllyLocalPositionToVisuals(WheelCollider collider)
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

    public void FixedUpdate()
    {

        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(rb.velocity * trust);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            rb.AddForce(rb.velocity * -trust); 
            rb.AddForce(rb.velocity * -trust);
        } 

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
    }
} 