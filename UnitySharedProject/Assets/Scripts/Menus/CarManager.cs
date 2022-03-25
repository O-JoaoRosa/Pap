using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarManager : MonoBehaviour
{
    [SerializeField] GameObject CarParent;
    static GameObject CarParentst;
    static GameObject CarParentstbck;
    [SerializeField] static Vector3 pos;
    [SerializeField] static Quaternion rot;

    public GameObject carId1;
    public GameObject carId2;
    public GameObject car3;
    public GameObject car4;
    public GameObject car5;
    public GameObject car6;
    public GameObject car0;



    static GameObject carId1st;
    static GameObject carId2st;
    static GameObject car3st;
    static GameObject car4st;
    static GameObject car5st;
    static GameObject car6st;
    static GameObject car0st;

    static GameObject carId1stbck;
    static GameObject carId2stbck;
    static GameObject car3stbck;
    static GameObject car4stbck;
    static GameObject car5stbck;
    static GameObject car6stbck;
    static GameObject car0stbck;
     

    public static void UpdateCarUsed() 
    {
        pos = Vector3.zero;
        pos.y = pos.y - 3f;
        rot = new Quaternion(0,0,0,0);

        if (GameObject.Find("CarRoot/CarModel/Car"))
        {
            Destroy(GameObject.Find("CarRoot/CarModel/Car"));
        }
        else
        {
            Destroy(GameObject.Find("CarRoot/CarModel/Car(Clone)"));
        }

        switch (Data.Player.UserCarIDSelected)
        {
            case 1:
                
                carId1st = Instantiate(carId1st, CarParentst.transform.position, rot, CarParentst.transform);

                if (SceneManager.GetActiveScene().name == "tutorialRace")
                {
                    carId1st.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                }

                carId1st.name = "Car";
                carId1st = null;
                ResetValues();
                break;

            case 2:
                
                carId2st = Instantiate(carId2st, CarParentst.transform.position, rot, CarParentst.transform);
                if (SceneManager.GetActiveScene().name == "tutorialRace")
                {
                    carId2st.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                }
                carId2st.name = "Car";
                carId2st = null;
                ResetValues();
                break;

            case 3:
               
                car3st = Instantiate(car3st, CarParentst.transform.position, rot, CarParentst.transform);
                if (SceneManager.GetActiveScene().name == "tutorialRace")
                {
                    car3st.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                }
                car3st.name = "Car";
                car3st = null;
                ResetValues();
                break;

            case 4:
                
                car4st = Instantiate(car4st, CarParentst.transform.position, rot, CarParentst.transform);
                if (SceneManager.GetActiveScene().name == "tutorialRace")
                {
                    car4st.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                }
                car4st.name = "Car";
                car4st = null;
                ResetValues();
                break;

            case 5:
                
                car5st = Instantiate(car5st, CarParentst.transform.position, rot, CarParentst.transform);
                if (SceneManager.GetActiveScene().name == "tutorialRace")
                {
                    car5st.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                }
                car5st.name = "Car";
                car5st = null;
                ResetValues();
                break;

            case 6:
                
                car6st = Instantiate(car6st, CarParentst.transform.position, rot, CarParentst.transform);
                if (SceneManager.GetActiveScene().name == "tutorialRace")
                {
                    car6st.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                }
                car6st.name = "Car";
                car6st = null;
                ResetValues();
                break;

            default:
                
                car0st = Instantiate(car0st, CarParentst.transform.position, rot, CarParentst.transform);
                if (SceneManager.GetActiveScene().name == "tutorialRace")
                {
                    car0st.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                }
                car0st.name = "Car";
                car0st = null;
                ResetValues();
                break;
        }
    }

    private void Start()
    {
        CarParentst = CarParent;
        car0st = car0;
        carId1st = carId1;
        carId2st = carId2;
        car3st = car3;
        car4st = car4;
        car5st = car5;
        car6st = car6;

        CarParentstbck = CarParent;
        car0stbck = car0;
        carId1stbck = carId1;
        carId2stbck = carId2;
        car3stbck = car3;
        car4stbck = car4;
        car5stbck = car5;
        car6stbck = car6;
        UpdateCarUsed();
    }
    public static void ResetValues()
    {
        CarParentst = CarParentstbck;
        car0st = car0stbck;
        carId1st = carId1stbck;
        carId2st = carId2stbck;
        car3st = car3stbck;
        car4st = car4stbck;
        car5st = car5stbck;
        car6st = car6stbck;
    }
}
