using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;

public class CarCustomizerMenuScript : MonoBehaviour
{
    public GameObject ChangeCarsMenu;
    public GameObject FirstMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        //adiciona a��es caso os triggers sejam ligados
        EventController.current.onCarSelectorEnter += ShowMenu;
    }

    // Update is called once per frame
    void Update()
    {
        //verifica se o bot�o esc foi carregado para sair da pausa
        if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    void ShowMenu()
    {
        ChangeCarsMenu.SetActive(false);

        gameObject.SetActive(true);
        FirstMenu.SetActive(true);
        for (int i = 1; i <= rarity; i++)
        {
            GameObject.Find("Star" + i);
        }
    }

    public void ChangeCarsButton()
    {
        FirstMenu.SetActive(false);
        ChangeCarsMenu.SetActive(true);
    }
}