using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceController : MonoBehaviour
{
    public Text timer;
    float time = 0;
    float timeMlSc = 0;
    float timeSc = 0;
    float timeMin = 0;
    float timeH = 0;
    bool isTimeCounting = false;
    private float StartTime;

    // Start is called before the first frame update
    void Start()
    {
        //associa o evento criado no script eventController com o metodo
        EventController.current.onRaceStartExit += StartRace;
        StartTime = Time.time;
    }


    private void Update()
    {
        if (isTimeCounting == true)
        {
            time = Time.time - StartTime;

            #region TimeConversion
            string mins = ((int)time / 60).ToString("00");
            string segs = (time % 60).ToString("00");
            string milisegs = ((time * 100) % 100).ToString("00");
            string TimerString = string.Format("{00}:{01}:{02}", mins, segs, milisegs);
            #endregion

            timer.text = $"{TimerString}";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventController.current.RaceStartExist();
        if (Data.activeGameMode == Data.GameModeTimeAttack)
        {
            isTimeCounting = true;
        }

    }

    void StartRace()
    {
        isTimeCounting = true;
    }
}
