using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceController : MonoBehaviour
{
    public check1trigger lastTriggerA;
    public check1trigger lastTriggerB;
    public Text timer;
    public Text lapCounter;
    int lap = 0;
    int totalLaps;
    float time = 0;
    string TimerString;
    bool isTimeCounting = false;
    private float StartTime;

    // Start is called before the first frame update
    void Start()
    {
        //associa o evento criado no script eventController com o metodo
        EventController.current.onRaceStartExit += StartRace;
        totalLaps = Data.Track.NumberOfLaps;
        lapCounter.text = $"LAP {lap}/{totalLaps}";
        Data.Track.lapTimes.Clear();
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
            TimerString = string.Format("{00}:{01}:{02}", mins, segs, milisegs);
            #endregion

            timer.text = $"{TimerString}";

            lapCounter.text = $"LAP {lap}/{totalLaps}";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventController.current.RaceStartExist();
    }

    private void OnTriggerEnter(Collider other)
    {
        //verifica se passou por todos os checks
        if (lastTriggerA.nmbrTChck == 100 || lastTriggerB.nmbrTChck == 21)
        {
            //salva o tempo na variavel
            Data.Track.lapTimes.Add(TimerString);
            StartTime = Time.time;

            //verifica se foi a ultima volta ou se deve adicionar mais uma
            if (lap == totalLaps)
            {
                EventController.current.OnRaceFinish();
                CarControl.canMove = false;
            }
            else
            {
                lap += 1;
                EventController.current.OnCrossingTheLine();
            }

        }

    }

    void StartRace()
    {
        isTimeCounting = true;

        Debug.Log("Active mode " + Data.Track.TypeOfRace);
        Debug.Log("Compared GameMode" + Data.GameModeTimeAttack);

        isTimeCounting = true;
        if (lap == 0)
        {
            Debug.Log("Time stored");
            StartTime = Time.time;
            lap += 1;
        }
    }
}
