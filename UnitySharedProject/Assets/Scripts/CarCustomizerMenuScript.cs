using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static Stats;

public class CarCustomizerMenuScript : MonoBehaviour
{
    public GameObject ChangeCarsMenu;
    public GameObject FirstMenu;
    public GameObject CarStatsMenu;
    public GameObject CarListMenu;

    [Header("Stats")]
    public GameObject AccelarationBar;
    public GameObject TurningBar;
    public GameObject MaxSpeedBar;
    public Text CarName;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        //adiciona ações caso os triggers sejam ligados
        EventController.current.onCarSelectorEnter += ShowMenu;
    }

    // Update is called once per frame
    void Update()
    {
        //verifica se o botão esc foi carregado para sair da pausa
        if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    void ShowMenu()
    {
        Thread.Sleep(500);
        ChangeCarsMenu.SetActive(false);

        gameObject.SetActive(true);
        FirstMenu.SetActive(true);
        for (int i = 1; i <= rarity; i++)
        {
            GameObject.Find("Star" + i);
        }
    }

    void ChangeCar()
    {
        FirstMenu.SetActive(false);
        CarStatsMenu.SetActive(true);
    }

    void NextCar()
    {
        
    }


    public void ChangeCarsButton()
    {
        FirstMenu.SetActive(false);
        ChangeCarsMenu.SetActive(true);
    }
}
