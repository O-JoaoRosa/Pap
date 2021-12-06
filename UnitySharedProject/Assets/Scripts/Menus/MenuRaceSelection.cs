using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRaceSelection : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);

        //associa o evento criado no script eventController com o metodo
        EventController.current.onGarageTriggerEnter += onGarageEnter;
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Escape)) && menu.activeSelf)
        {
            menu.SetActive(false);
        }
    }

    //metodo que é chamado quando o evento é acionado
    void onGarageEnter()
    {
        menu.SetActive(true);
    }
}
