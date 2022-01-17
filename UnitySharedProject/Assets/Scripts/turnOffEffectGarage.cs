using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffEffectGarage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //adiciona ações caso os triggers sejam ligados
        EventController.current.onCarSelectorEnter += turnEffectsOff;
        EventController.current.onCarSelectorExit += turnEffectsOn;
    }

    /// <summary>
    /// desliga os efeitos da garagem
    /// </summary>
    void turnEffectsOff()
    {
        Debug.Log("efeitos pausa");
        gameObject.GetComponent<ParticleSystem>().Stop();
    }
    
    /// <summary>
    /// liga os efeitos da garagem
    /// </summary>
    void turnEffectsOn()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
