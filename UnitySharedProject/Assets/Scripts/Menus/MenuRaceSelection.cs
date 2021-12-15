using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRaceSelection : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    void Start()
    {
        menu.SetActive(false);

        //associa o evento criado no script eventController com o metodo
        EventController.current.onGarageTriggerEnter += onGarageEnter;
    }

    private void Update()
    {
        //verifica se o botão esc foi carregado para sair da pausa
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
    /// metodo que é chamado quando o evento é acionado
    /// </summary>
    void onGarageEnter()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
    }
}
