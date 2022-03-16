using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public new Camera camera;

    public GameObject car;
    public GameObject carSelectionLookAt;

    private static bool islooking = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (islooking)
        {
            camera.transform.LookAt(car.transform);
        }
        else
        {

            Debug.Log("isLooking false");
            camera.transform.LookAt(carSelectionLookAt.transform);
        }
    }

    public static void StartLooking()
    { 
        islooking = true;
    }

    static public void StopLooking()
    {
        Debug.Log("isLooking false");
        islooking = false;
    }
}
