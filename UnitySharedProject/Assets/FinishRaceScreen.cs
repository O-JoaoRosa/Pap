using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishRaceScreen : MonoBehaviour
{
    [Header("Finish")]
    public GameObject Finish;

    [Header("Stars")]
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    [Header("Times")]
    public Text TimeLap1;
    public Text TimeLap2;
    public Text TimeLap3;

    [Header("Money")]
    public Text MoneyGained;
    public Text MoneyShadow;

    bool isLapTimeUpdatable = false;
    bool isMoneyUpdatable = false;
    bool updateStars = false;

    int nStars = 0;
    private int lapToUpdate = 0;
    float secs;
    float milisecs;
    float min;

    float mili;
    float sec;
    float minu;


    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
        EventController.current.onRaceFinish += RaceEnd;
    }

    // Update is called once per frame
    void Update()
    {
        MoneyShadow.text = MoneyGained.text;
    }

    private void FixedUpdate()
    {

        if (isLapTimeUpdatable)
        {
            #region Updates nos tempos
            Text txt = TimeLap1;
            switch (lapToUpdate)
            {
                case 0:
                    txt = TimeLap1;
                    break;
                case 1:
                    txt = TimeLap2;
                    break;
                case 2:
                    txt = TimeLap3;
                    break;
                default:
                    lapToUpdate = 0;
                    isLapTimeUpdatable = false;
                    isMoneyUpdatable = true;
                    updateStars = true;
                    break;
            }

            if (mili < milisecs)
            {
                mili += 1;
                txt.text = $"00 : 00 : {mili}";
            }
            else if (sec < secs)
            {
                sec += 1;
                txt.text = $"00 : {sec} : {mili}";
            }
            else if (minu < min)
            {
                minu += 1;
                txt.text = $"{minu} : {sec} : {mili}";
                if (min < 1 && secs < 59)
                {
                    nStars += 1;
                }
            }
            else
            {
                lapToUpdate += 1;

                mili = 00;
                sec = 00;
                minu = 00;

                milisecs = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[2]);
                secs = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[1]);
                min = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[0]);
            }
            #endregion
        }
        else if (isMoneyUpdatable)
        {
            if (updateStars)
            {
                switch (nStars)
                {
                    case 0:
                        MoneyGained.text = "+15";
                        break;
                    case 1:
                        MoneyGained.text = "+50";
                        LeanTween.color(Star1, Color.white, 0.5f);
                        break;
                    case 2:

                        MoneyGained.text = "+100";
                        LeanTween.color(Star1, Color.white, 0.5f);
                        LeanTween.color(Star2, Color.white, 0.5f);
                        break;
                    case 3:
                        MoneyGained.text = "+150";
                        LeanTween.color(Star1, Color.white, 0.5f);
                        LeanTween.color(Star2, Color.white, 0.5f);
                        LeanTween.color(Star3, Color.white, 0.5f);
                        break;
                    default:
                        break;
                }

                updateStars = false;
            }

            string money = MoneyGained.text.Replace("+", "");

            if (nStars > 0 && int.Parse(money) != 0 )
            {
                Data.Player.Money += 1;
                MoneyGained.text = "+" + (int.Parse(money) - 1).ToString();
            } 
        }
    }

    void ContinueButton()
    {
        SceneManager.LoadSceneAsync("GarageLobby", LoadSceneMode.Single);
    }

    void RaceEnd()
    {
        mili = 00;
        sec = 00;
        minu = 00;

        milisecs = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[0]);
        secs = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[1]);
        min = float.Parse(Data.Track.lapTimes[lapToUpdate].Split(':')[2]);

        gameObject.SetActive(true);

        Finish.LeanAlpha(1f, 0.3f);
        Finish.LeanScale(new Vector3(0.344f, 0.344f, 0.344f), 1f);
        isLapTimeUpdatable = true;
    }

}
