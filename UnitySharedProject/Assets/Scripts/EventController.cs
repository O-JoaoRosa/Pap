using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController current;

    private void Awake()
    {
        current = this;
    }

    //cria um evento que ira verificar se o jogador entorou na garagem
    public event Action onGarageTriggerEnter;

    public void GarageTriggerEnter()
    {
        if (onGarageTriggerEnter != null)
        {
            onGarageTriggerEnter();
        }
    }

    //cria um evento que ira verificar se o jogador Saiu da garagem
    public event Action onGarageTriggerExit;

    public void GarageTriggerExit()
    {
        if (onGarageTriggerExit != null)
        {
            onGarageTriggerExit();
        }
    }
}
