using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] GameObject CarParent;
    static GameObject CarParentst;
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
     

    public static void UpdateCarUsed() 
    {
        pos = CarParentst.transform.GetChild(0).localPosition;
        rot = CarParentst.transform.GetChild(0).rotation;

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
                carId1st.transform.localScale = new Vector3(0.38f, 0.38f, 0.38f);
                carId1st = Instantiate(carId1st, CarParentst.transform.position, rot, CarParentst.transform);
                carId1st.name = "Car";
                break;

            case 2:
                carId2st = Instantiate(carId2st, CarParentst.transform.position, rot, CarParentst.transform);
                carId2st.name = "Car";
                break;

            case 3:
                car3st = Instantiate(car3st, CarParentst.transform.position, rot, CarParentst.transform);
                car3st.name = "Car";
                break;

            case 4:
                car4st = Instantiate(car4st, CarParentst.transform.position, rot, CarParentst.transform);
                car4st.name = "Car";
                break;

            case 5:
                car5st = Instantiate(car5st, CarParentst.transform.position, rot, CarParentst.transform);
                car5st.name = "Car";
                break;

            case 6:
                car6st = Instantiate(car6st, CarParentst.transform.position, rot, CarParentst.transform);
                car6st.name = "Car";
                break;

            default:
                car0st = Instantiate(car0st, CarParentst.transform.position, rot, CarParentst.transform);
                car0st.name = "Car";
                break;
        }
    }

    private void Awake()
    {
        CarParentst = CarParent;
        car0st = car0;
        carId1st = carId1;
        carId2st = carId2;
        car3st = car3;
        car4st = car4;
        car5st = car5;
        car6st = car6;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCarUsed();
    }
}
