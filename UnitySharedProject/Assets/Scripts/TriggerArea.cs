using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    bool canTrigger = true;
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        //Checks if he can trigger the menu again or not
        if (canTrigger)
        {
            EventController.current.GarageTriggerEnter();
            canTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //miranha
        anim.SetTrigger("Reset");
        EventController.current.GarageTriggerExit();
        canTrigger = true;

    }
}
