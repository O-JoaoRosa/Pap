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


    // Start is called before the first frame update
    void Start()
    {
        //verifica qual o modo de jogo activo
        if (Data.activeGameMode == Data.GameModeStory)
        {
            Time.timeScale = 0.1f;

            //faz a anima��o inicial da historia
            bigScreen.LeanScale(new Vector3(1, 1, 1), 0.5f).setIgnoreTimeScale(true);
            
            //adiciona � lista a historia
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
            isTextShowing = true;
            bigScreen.SetActive(true);
            smallScreen.SetActive(false);

            Debug.Log(messages[numOfMessages]);
        }
    }


    private void Update()
    {
        ChangeText();
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

        //muda o texto quando carregam no bot�o
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Mouse0)) && isTextShowing)
        {
            if (numOfMessages == 10)
            {
                isTextShowing = false;
                Time.timeScale = 1;
            }
            else if (numOfMessages == 12)
            {
                isTextShowing = false;
                Time.timeScale = 1;
            }
            numOfMessages += 1;

            Debug.Log(numOfMessages);
        }

    }
}
