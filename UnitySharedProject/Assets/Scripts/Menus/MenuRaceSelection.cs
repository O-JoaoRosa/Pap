using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRaceSelection : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    //TODO : acabar a mudan�a de pistas com o menu 

    void Start()
    {
        menu.SetActive(false);

        //associa o evento criado no script eventController com o metodo
        EventController.current.onGarageTriggerEnter += onGarageEnter;
    }

    private void Update()
    {
        //verifica se o bot�o esc foi carregado para sair da pausa
        if ((Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Escape)) && menu.activeSelf)
        {

            CloseMenu();
        }
    }

    /// <summary>
    /// fecha o menu e resume o tempo
    /// </summary>
    public void CloseMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// metodo que � chamado quando o evento � acionado
    /// </summary>
    void onGarageEnter()
    {
        menu.SetActive(true);
        menu.SetActive(true);
        Time.timeScale = 0;
    }
}
