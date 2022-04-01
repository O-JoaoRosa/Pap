using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public GameObject bigScreen;
    public GameObject smallScreen;
    public Text text;
    public Text tinyText;
    public static int numOfMessages = 0;
    public static List<string> messages = new List<string>();
    public static bool isTextShowing = true;
    public GameObject TempBarrier;


    // Start is called before the first frame update
    void Start()
    {
        //verifica qual o modo de jogo activo
        if (Data.Track.TypeOfRace == Data.GameModeStory)
        {
            Time.timeScale = 0.1f;

            //faz a animação inicial da historia
            bigScreen.LeanScale(new Vector3(1, 1, 1), 0.5f).setIgnoreTimeScale(true);
            
            //adiciona à lista a historia
            messages.Add("OH!"); //0
            messages.Add("I guess we have a new wanna-be racer"); //1
            messages.Add("Well, welcome to Road Rush industries, we here offer the best entertainment on the planet and bla bla bla"); //2
            messages.Add("I am legally obligated to informe you that we do not take responsibility for the break of bones, loss of organ function, dessies and sicknesses that might come with the " +
                "inhalation of toxic fumes, overall harm and/or death"); //3
            messages.Add("Anyways that doesnt matter, I hope your ready for whats coming for you!"); //4
            messages.Add("Here Have this so its easier to talk to you!"); //5
            messages.Add("By the way you can call me coach frank"); //6
            messages.Add("Alright lets test your skills..."); //7
            messages.Add("I'll let you borrow this car here"); //8
            messages.Add("You at least know how to drive right?"); //9
            messages.Add("Of course you know you can use 'W A S D' to drive and steer right?"); //10
            messages.Add("And you also know that you can drift if you press 'Space' while turning right?"); //11
            messages.Add("I mean only idiots don't know that! HA HA!"); //12
            messages.Add("Anyways i see that you aren't a complete idiot"); //13
            messages.Add("Let me see how you handle those thight corners"); //14
            messages.Add("Meh.. i've seen better "); //15
            messages.Add("you see that icon on the bottom left of the screen?"); //16
            messages.Add("whell at least you arent blind, that's your power up!"); //17
            messages.Add("It fills up while you drift and can be activated with 'Shift'"); //18
            messages.Add("Well, thats all i got to teach you i guess"); //19
            messages.Add("Ups I forgot to mention but the red road blocks aren't imovable"); //20
            messages.Add("Anyways good luck finishing the race!"); //21

            isTextShowing = true;
            bigScreen.SetActive(true);
            smallScreen.SetActive(false);

            Debug.Log(messages[numOfMessages]);
        }
        else
        {
            TempBarrier.SetActive(false);
        }
    }


    private void Update()
    {
        if (Data.Track.TypeOfRace == Data.GameModeStory)
        {
            ChangeText();
        }
    }

    void ChangeText()
    {
        if (isTextShowing && numOfMessages >= 5)
        {
            text = tinyText;
            smallScreen.SetActive(true);
            bigScreen.SetActive(false);
        }
        else if (isTextShowing && numOfMessages < 5)
        {
            bigScreen.SetActive(true);
            smallScreen.SetActive(false);
        }
        else
        {
            bigScreen.SetActive(false);
            smallScreen.SetActive(false);
        }

        //mete o texto do tutorial na inteface
        text.text = messages[numOfMessages];

        //muda o texto quando carregam no botão
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Mouse0)) && isTextShowing)
        {
            if (numOfMessages == 10)
            {
                check1trigger.TutorialCheck = 1;
                isTextShowing = false;
                Time.timeScale = 1;
            }
            else if (numOfMessages == 12)
            {
                check1trigger.TutorialCheck = 2;
                isTextShowing = false;
                Time.timeScale = 1;
            }
            else if (numOfMessages == 14)
            {
                check1trigger.TutorialCheck = 10;
                isTextShowing = false;
                Time.timeScale = 1;
            }
            else if (numOfMessages == 18)
            {
                check1trigger.TutorialCheck = 17;
                isTextShowing = false;
                Time.timeScale = 1;
            }
            else if (numOfMessages == 21)
            {
                check1trigger.TutorialCheck = 100;
                TempBarrier.SetActive(false);
                isTextShowing = false;
                Time.timeScale = 1;
            }
            numOfMessages += 1;

            Debug.Log(numOfMessages);
        }

    }
}
