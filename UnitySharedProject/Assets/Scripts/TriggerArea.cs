using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    bool canTrigger = true;
    public GameObject menuBackground;

    private void OnTriggerEnter(Collider other)
    {
        //Checks if he can trigger the menu again or not
        if (canTrigger)
        {
            menuBackground.SetActive(true);
            EventController.current.GarageTriggerEnter();
            canTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //miranha
        menuBackground.transform.localPosition = new Vector3(0, 0, 0);
        EventController.current.GarageTriggerExit();
        canTrigger = true;

    }
}
