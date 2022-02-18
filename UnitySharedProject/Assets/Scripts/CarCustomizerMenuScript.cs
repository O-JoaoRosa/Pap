using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static Stats;

public class CarCustomizerMenuScript : MonoBehaviour
{
    private int idInicial;
    private bool isMenuActive;
    public GameObject CustomizeMenu;
    public GameObject FirstMenu;
    public GameObject CarStatsMenu;
    public GameObject CarListMenu;
    Vector3 pos;

    [Header("Buttons")]
    public GameObject ButtonChangeCar;
    public GameObject ButtonCustomize;
    public GameObject ButtonFullLis;

    private GameObject Car0;
    private GameObject Car1;
    private GameObject Car2;
    private GameObject car3;

    [Header("Prefabs")]
    public GameObject car0Prefab;
    public GameObject car1Prefab;
    public GameObject car2Prefab;
    public GameObject car3Prefab;


    [Header("Stats")]
    public Slider AccelarationBar;
    public Slider TurningBar;
    public Slider MaxSpeedBar;
    public Text CarName;

    [Header("Fisrt Menu")]
    public Text FirstMenuCarName;

    [Header("Menu Cars List")]
    public Text CarsListName;

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
        if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Escape) && isMenuActive)
        {
            gameObject.SetActive(false);
            FirstMenu.SetActive(false);
            CustomizeMenu.SetActive(false);
            CarStatsMenu.SetActive(false);
            CarListMenu.SetActive(false);
            Data.Player.UserCarIDSelected = idInicial;
        }

        //muda as barras para mostrar os stats dos carros
        AccelarationBar.value = Data.ActiveCar.FowardSpeed;
        TurningBar.value = (Data.ActiveCar.DriftTurnAngle + Data.ActiveCar.DefaultTurnAngle) / 2 ;
        MaxSpeedBar.value = -Data.ActiveCar.GroundDrag;

        //muda os nomes
        CarName.text = (Data.ActiveCar.Descri).ToUpper();
        FirstMenuCarName.text = (Data.ActiveCar.Descri).ToUpper();
        CarsListName.text = (Data.ActiveCar.Descri).ToUpper();
    }

    /// <summary>
    /// metodo que é executado quando o menu 
    /// </summary>
    void ShowMenu()
    {
        idInicial = Data.Player.UserCarIDSelected;

        Thread.Sleep(500);
        isMenuActive = true;
        gameObject.SetActive(true);
        //CustomizeMenu.SetActive(false);
        CarStatsMenu.SetActive(false);
        CarListMenu.SetActive(false);
        FirstMenu.SetActive(true);
        ChangeRarity();

    }

    void ChangeRarity()
    {
        for (int i = 1; i <= Data.ActiveCar.Rarity; i++)
        {
            Debug.Log(Data.ActiveCar.Rarity);
            GameObject.Find("Canvas/Menus/RawImage/Title/Star" + i).GetComponent<Image>().color = Color.white;
            GameObject.Find("Canvas/CarListMenuBackground/CarName/Star" + i).GetComponent<Image>().color = Color.white;
        }

        for (int i = 4; i > Data.ActiveCar.Rarity; i--)
        {
            ChangeRarity();
            GameObject.Find("Canvas/Menus/RawImage/Title/Star" + i).GetComponent<Image>().color = Color.gray;
            GameObject.Find("Canvas/CarListMenuBackground/CarName/Star" + i).GetComponent<Image>().color = Color.gray;
        }
    }

    public void ShowCarStats()
    {
        gameObject.SetActive(true);
        CarStatsMenu.SetActive(true);
        FirstMenu.SetActive(false);
    }


    #region Next/Previous Car

    /// <summary>
    /// muda de carro 
    /// </summary>
    public void NextCar()
    {

        switch (Data.ActiveCar.ID)
        {
            case 0:

                if (GameObject.Find("Car(Clone)"))
                {
                    pos = GameObject.Find("Car(Clone)").transform.position;
                    Destroy(GameObject.Find("Car(Clone)"));
                }
                else
                {
                    pos = GameObject.Find("Car").transform.position;
                    Destroy(GameObject.Find("Car"));
                }
                Car1 = Instantiate(car1Prefab, pos, new Quaternion(0, 0f, 0, 0));
                Car1.LeanRotate(new Vector3(0, -144f, 0), 0f);
                Car1.name = "Car";
                Data.Player.UserCarIDSelected = 1;

                break;

            case 1:

                if (GameObject.Find("Car(Clone)"))
                {
                    pos = GameObject.Find("Car(Clone)").transform.position;
                    Destroy(GameObject.Find("Car(Clone)"));
                }
                else
                {
                    pos = GameObject.Find("Car").transform.position;
                    Destroy(GameObject.Find("Car"));
                }
                Car2 = Instantiate(car2Prefab, pos, new Quaternion(0, 0f, 0, 0));
                Car2.LeanRotate(new Vector3(0, -144f, 0), 0f);
                Car2.name = "Car";
                Data.Player.UserCarIDSelected = 2;

                break;

            case 2:

                if (GameObject.Find("Car(Clone)"))
                {
                    pos = GameObject.Find("Car(Clone)").transform.position;
                    Destroy(GameObject.Find("Car(Clone)"));
                }
                else
                {
                    pos = GameObject.Find("Car").transform.position;
                    Destroy(GameObject.Find("Car"));
                }
                car3 = Instantiate(car3Prefab, pos, new Quaternion(0, 0f, 0, 0));
                car3.LeanRotate(new Vector3(0, -144f, 0), 0f);
                car3.name = "Car";
                Data.Player.UserCarIDSelected = 3;

                break;

            case 3:

                if (GameObject.Find("Car(Clone)"))
                {
                    pos = GameObject.Find("Car(Clone)").transform.position;
                    Destroy(GameObject.Find("Car(Clone)"));
                }
                else
                {
                    pos = GameObject.Find("Car").transform.position;
                    Destroy(GameObject.Find("Car"));
                }
                Car0 = Instantiate(car0Prefab, pos, new Quaternion(0, 0f, 0, 0));
                Car0.LeanRotate(new Vector3(0, -144f, 0), 0f);
                Car0.name = "Car";
                Data.Player.UserCarIDSelected = 0;

                break;

            default:
                break;
        }
        ChangeRarity();
    }


    public void PreviousCar()
    {

        switch (Data.ActiveCar.ID)
        {
            case 0:

                if (GameObject.Find("Car(Clone)"))
                {
                    pos = GameObject.Find("Car(Clone)").transform.position;
                    Destroy(GameObject.Find("Car(Clone)"));
                }
                else
                {
                    pos = GameObject.Find("Car").transform.position;
                    Destroy(GameObject.Find("Car"));
                }
                car3 = Instantiate(car3Prefab, pos, new Quaternion(0, 0f, 0, 0));
                car3.LeanRotate(new Vector3(0, -144f, 0), 0f);
                car3.name = "Car";
                Data.Player.UserCarIDSelected = 3;

                break;

            case 1:

                if (GameObject.Find("Car(Clone)"))
                {
                    pos = GameObject.Find("Car(Clone)").transform.position;
                    Destroy(GameObject.Find("Car(Clone)"));
                }
                else
                {
                    pos = GameObject.Find("Car").transform.position;
                    Destroy(GameObject.Find("Car"));
                }
                Car0 = Instantiate(car0Prefab, pos, new Quaternion(0, 0f, 0, 0));
                Car0.LeanRotate(new Vector3(0, -144f, 0), 0f);
                Car0.name = "Car";
                Data.Player.UserCarIDSelected = 0;

                break;

            case 2:

                if (GameObject.Find("Car(Clone)"))
                {
                    pos = GameObject.Find("Car(Clone)").transform.position;
                    Destroy(GameObject.Find("Car(Clone)"));
                }
                else
                {
                    pos = GameObject.Find("Car").transform.position;
                    Destroy(GameObject.Find("Car"));
                }
                Car1 = Instantiate(car1Prefab, pos, new Quaternion(0, 0f, 0, 0));
                Car1.LeanRotate(new Vector3(0, -144f, 0), 0f);
                Car1.name = "Car";
                Data.Player.UserCarIDSelected = 1;

                break;

            case 3:

                if (GameObject.Find("Car(Clone)"))
                {
                    pos = GameObject.Find("Car(Clone)").transform.position;
                    Destroy(GameObject.Find("Car(Clone)"));
                }
                else
                {
                    pos = GameObject.Find("Car").transform.position;
                    Destroy(GameObject.Find("Car"));
                }
                Car2 = Instantiate(car2Prefab, pos, new Quaternion(0, 0f, 0, 0));
                Car2.LeanRotate(new Vector3(0, -144f, 0), 0f);
                Car2.name = "Car";
                Data.Player.UserCarIDSelected = 2;

                break;

            default:
                break;
        }
        ChangeRarity();
    } 
    #endregion

    public void ShowAllCars()
    {
        CarStatsMenu.SetActive(false);
        CarListMenu.SetActive(true);
    }

    public void ChangeCarsButton()
    {
        FirstMenu.SetActive(false);
        CarStatsMenu.SetActive(true);
    }
}
