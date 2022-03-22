using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        EventController.current.onRaceFinish += RaceEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RaceEnd()
    {
        gameObject.SetActive(false);
    }
}
