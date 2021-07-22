using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float cruentSpeed;
    float boostSpeed;
    float maxSpeed;
    float realSpeed;
    float acceleration;

    [SerializeField] Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {
        realSpeed = transform.InverseTransformDirection(rb.velocity).z;

        if (Input.GetKey(KeyCode.W))
        {
            cruentSpeed = Mathf.Lerp(cruentSpeed, maxSpeed, Time.deltaTime * acceleration);
        }
    }
}
