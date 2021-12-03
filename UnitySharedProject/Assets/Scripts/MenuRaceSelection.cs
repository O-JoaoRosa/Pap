using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRaceSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //associa o evento criado no script eventController com o metodo
        EventController.current.onGarageTriggerEnter += onGarageEnter;
    }

    //metodo que é chamado quando o evento é acionado
    void onGarageEnter()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
