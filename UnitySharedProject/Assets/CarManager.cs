using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] GameObject CarParent;
    [SerializeField] Vector3 pos;
    [SerializeField] Quaternion rot;

    [Header("Models")][SerializeField]
    GameObject car1;
    GameObject car2;
    GameObject car3;
    GameObject car4;
    GameObject car5;
    GameObject car6;
    GameObject car0;

    // Start is called before the first frame update
    void Awake()
    {
        pos = CarParent.transform.GetChild(0).localPosition;
        rot = CarParent.transform.GetChild(0).rotation;
        Destroy(CarParent.transform.GetChild(0));
        switch (Data.Player.userCarIDSelected)
        {
            case 1:
                car1 = Instantiate(car1,CarParent.transform.position, rot, CarParent.transform);
                break; 

            case 2:
                car2 = Instantiate(car2, CarParent.transform.position, rot, CarParent.transform);
                break;

            case 3:
                car3 = Instantiate(car3, CarParent.transform.position, rot, CarParent.transform);
                break;

            case 4:
                car4 = Instantiate(car4, CarParent.transform.position, rot, CarParent.transform);
                break;

            case 5:
                car5 = Instantiate(car5, CarParent.transform.position, rot, CarParent.transform);
                break;

            case 6:
                car6 = Instantiate(car6, CarParent.transform.position, rot, CarParent.transform);
                break;

            default:
                car0 = Instantiate(car0, CarParent.transform.position, rot, CarParent.transform);
                break;
        }
    }
}
