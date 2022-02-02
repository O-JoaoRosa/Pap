using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public Text text;
    private int numOfMessages = 0;
    private List<string> messages;

    // Start is called before the first frame update
    void Start()
    {
        messages.Add("OH!");
        messages.Add("I guess we have a new wanna-be racer");
        messages.Add("Well, welcome to Road Rush industries, we here offer the best entertainment on the planet and bla bla bla");
        messages.Add("I am legally obligated to informe you that we do not take responsibility for the break of bones, loss of limbs or organ function, dessies and sicknesses that might come with the " +
            "inhalation of toxic fumes, overall harm and/or death");
        messages.Add("Anyways that doesnt matter, I hope your ready for whats coming for you!");
        messages.Add("Alright lets test your skills...");
        messages.Add("I'll let you borrow this car here");
        messages.Add("You at least know how to drive right?");
        messages.Add("Of coures you know you can use 'W A S D' to drive or the arrow keys right?");
        messages.Add("And you also know that you can drift if you press 'Space' while turning right? I mean only idiots don't know that! HA HA!");



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
