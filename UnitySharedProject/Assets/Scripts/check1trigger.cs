using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class check1trigger : MonoBehaviour
{
    int checkCount = 0;
    public static int numberToCheck = 1;
    public int nmbrTChck;

    // Start is called before the first frame update
    void Start()
    {
        //associa o evento criado no script eventController com o metodo
        if (Data.Track.TypeOfRace == Data.GameModeStory)
        {
            EventController.current.onCheck1 += ShowCheck1;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name.Contains(numberToCheck.ToString()))
        {
            numberToCheck += 1;
            nmbrTChck = numberToCheck;
        }
        else if (gameObject.name.Contains("15") && numberToCheck == 12)
        {
            numberToCheck = 16;
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
}
