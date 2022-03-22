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
    public GameObject CheckPoint;
    public bool isTimeCouting = false;
    bool isThisObject = false;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        CheckPoint = transform.GetChild(0).gameObject;
        CheckPoint.SetActive(false);
        distance = Vector3.Distance(plyr.transform.position, gameObject.transform.position);
        lastClosest = distance + 10f;
        //associa o evento criado no script eventController com o metodo
        if (Data.Track.TypeOfRace == Data.GameModeStory)
        {
            EventController.current.onCheck1 += ShowCheck1;
            EventController.current.onCrossingTheLine += ResetValues;
        }
       
    }

    private void Update()
    {
        isThisObject = gameObject == GameObject.Find($"Check{numberToCheck}-") ? true : false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isThisObject)
        {
            distance = Vector3.Distance(plyr.transform.position, gameObject.transform.position);
            if (distance < lastClosest)
            {
                lastClosest = distance;
                wrongWaySignal.SetActive(false);
                Debug.LogWarning("Getting closer : " + lastClosest + " Object name : " + gameObject.name);
            }
            else if (distance > lastClosest)
            {
                if (!isTimeCouting)
                {
                    time = Time.time;
                    isTimeCouting = true;
                }

                if(((Time.time - time) > 10f) && isTimeCouting)
                {
                    CheckPoint.SetActive(true);
                }
                else if(!isTimeCouting)
                {
                    CheckPoint.SetActive(false);
                }

                lastClosest = distance;
                wrongWaySignal.SetActive(true);
                Debug.LogWarning("going Back : " + lastClosest + " Object name : " + gameObject.name);
            }
        }
        else
        {
            CheckPoint.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains(numberToCheck.ToString() + "-"))
        {
            CheckPoint.SetActive(false);
            numberToCheck += 1;
            nmbrTChck = numberToCheck;
            if (numberToCheck == 18)
            {
                ResetValues();
            }

            isTimeCouting = false;

        }
        else if (gameObject.name.Contains("18-") && numberToCheck == 14)
        {
            CheckPoint.SetActive(false);
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
