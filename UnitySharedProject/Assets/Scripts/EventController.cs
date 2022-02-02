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

    #region Garagem

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

    public event Action onCarSelectorEnter;
    public void CarSelectorEnter()
    {
        if (onCarSelectorEnter != null)
        {
            onCarSelectorEnter();
        }
    }

    public event Action onCarSelectorExit;
    public void CarSelectorExit()
    {
        if (onCarSelectorExit != null)
        {
            onCarSelectorExit();
        }
    }
    
    #endregion

    public event Action onRaceStartExit;
    public void RaceStartExist()
    {
        if (onRaceStartExit != null)
        {
            onRaceStartExit();
        }
    }
}
