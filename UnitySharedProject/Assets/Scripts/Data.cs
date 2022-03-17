using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static string GameModeStory = "Story";
    public static string GameModeTimeAttack = "Time Attack";
    public static string GameModeVs = "Vs";
    public static RaceInfo Track = new RaceInfo();
    public RaceInfo trck;
    public static jsonData Player;
    public static CarData ActiveCar;
    public static List<CarData> cars = new List<CarData>();
    [SerializeField]public jsonData plyr;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DoNotDestroy");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        plyr = Player;

        if (Player != null)
        {
            trck = Track;

            if (ActiveCar != null)
            {
                if (ActiveCar.ID != Player.UserCarIDSelected)
                {
                    Debug.Log("mudança no carro selecionado encontrada");
                    ActiveCar = cars[Player.UserCarIDSelected];
                    Debug.Log(ActiveCar.ID);
                    Debug.Log(ActiveCar.FowardSpeed);
                    Debug.Log(ActiveCar.DefaultTurnAngle);
                    Debug.Log(ActiveCar.TurnSpeed);
                    Debug.Log(ActiveCar.DriftTurnAngle);
                }
            }
            else
            {
                ActiveCar = cars[Player.UserCarIDSelected];
                Debug.Log(ActiveCar.ID);
                Debug.Log(ActiveCar.FowardSpeed);
                Debug.Log(ActiveCar.DefaultTurnAngle);
                Debug.Log(ActiveCar.TurnSpeed);
                Debug.Log(ActiveCar.DriftTurnAngle);
            }
        }
        
    }
}
