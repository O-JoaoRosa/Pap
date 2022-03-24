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

    public event Action onCheck1;
    public void Check1Exit()
    {
        if (onCheck1 != null)
        {
            onCheck1();
        }
    }

    public event Action onCheck2;
    public void Check2Exit()
    {
        if (onCheck2 != null)
        {
            onCheck2();
        }
    }


    public event Action onCrossingTheLine;
    public void OnCrossingTheLine()
    {
        if (onCrossingTheLine != null)
        {
            onCrossingTheLine();
        }
    }

    public event Action onRaceFinish;
    public void OnRaceFinish()
    {
        if (onRaceFinish != null)
        {
            onRaceFinish();
        }
    }

    public event Action onResetActions;
    public void OnResetActions()
    {
        onCarSelectorEnter = null;
        onCarSelectorExit = null;
        onCheck1 = null;
        onCheck2 = null;
        onCrossingTheLine = null;
        onGarageTriggerEnter = null;
        onGarageTriggerExit = null;
        onRaceFinish = null;
        onRaceStartExit = null;
        onResetActions = null;
    }
}
