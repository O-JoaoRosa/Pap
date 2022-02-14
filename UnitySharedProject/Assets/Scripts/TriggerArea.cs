using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    bool canTrigger = true;
    public GameObject menuBackground;

    private void Start()
    {
        menuBackground.SetActive(false);
    }

    private void Update()
    {
        //verifica se o botão esc foi carregado para sair da pausa
        if ((Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Escape)) && menuBackground.activeSelf)
        {
            CloseMenu();
        }
    }

    /// <summary>
    /// fecha o menu e resume o tempo
    /// </summary>
    public void CloseMenu()
    {
        menuBackground.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if he can trigger the menu again or not
        if (canTrigger)
        {
            Debug.LogWarning("entrou na garagem");
            menuBackground.SetActive(true);

            EventController.current.GarageTriggerEnter();
            Time.timeScale = 0;
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
