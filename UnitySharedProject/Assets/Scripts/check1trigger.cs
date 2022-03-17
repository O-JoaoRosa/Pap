using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class check1trigger : MonoBehaviour
{
    int checkCount = 0;
    public static int numberToCheck = 1;
    public int nmbrTChck;
    public float distance;
    public float lastClosest;
    public GameObject plyr;
    public GameObject wrongWaySignal;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(plyr.transform.position, gameObject.transform.position);
        lastClosest = distance;
        //associa o evento criado no script eventController com o metodo
        if (Data.Track.TypeOfRace == Data.GameModeStory)
        {
            EventController.current.onCheck1 += ShowCheck1;
            EventController.current.onCrossingTheLine += ResetValues;
        }
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.name.Contains(nmbrTChck.ToString() + "-"))
        {
            GameObject obj = GameObject.Find("Check" + numberToCheck + "-");
            distance = Vector3.Distance(plyr.transform.position, obj.transform.position);
            if (distance < lastClosest)
            {
                lastClosest = distance;
                wrongWaySignal.SetActive(false);
                Debug.LogWarning("Getting closer : " + lastClosest + " Object name : " + obj.name);
            }
            else if (distance > lastClosest)
            {
                lastClosest = distance;
                wrongWaySignal.SetActive(true);
                Debug.LogWarning("going Back : " + lastClosest + " Object name : " + obj.name);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains(numberToCheck.ToString() + "-"))
        {
            numberToCheck += 1;
            nmbrTChck = numberToCheck;
            if (numberToCheck == 18)
            {
                ResetValues();
            }

        }
        else if (gameObject.name.Contains("18-") && numberToCheck == 14)
        {
            numberToCheck = 19;
            nmbrTChck = numberToCheck;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Data.Track.TypeOfRace == Data.GameModeStory)
        {
            EventController.current.Check1Exit();
        }
    }

    public void ShowCheck1()
    {
        Time.timeScale = 0.1f;
        tutorial.isTextShowing = true;
    }

    public void ResetValues()
    {
        nmbrTChck = 1;
        numberToCheck = 1;
    }
}
