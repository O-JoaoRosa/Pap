using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public new Camera camera;

    public GameObject car;

    private bool islooking = true;

    // Start is called before the first frame update
    void Start()
    {
        //adiciona ações caso os triggers sejam ligados
        EventController.current.onCarSelectorEnter += StopLooking;
        EventController.current.onCarSelectorExit += StartLooking;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (islooking)
        {
            camera.transform.LookAt(car.transform);
        }
    }

    private void StartLooking()
    {
        islooking = true;
    }

    private void StopLooking()
    {
        islooking = false;
    }
}
