using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraScript : MonoBehaviour
{
    public Rigidbody target;
    public float speedOfSphere;

    private Vector3 initialPossition;
    private float lastMaxSpeed = 0f;


    // Start is called before the first frame update
    void Start()
    {
        initialPossition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speedOfSphere = target.velocity.magnitude;

        if (target.velocity.magnitude >= lastMaxSpeed)
        {
            transform.position = Vector3.Lerp(initialPossition, transform.position - new Vector3(0, 0, 2), Time.deltaTime);
        }
        else if(target.velocity.magnitude < lastMaxSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, initialPossition, Time.deltaTime);
        }

        lastMaxSpeed = target.velocity.magnitude;
    }
}
