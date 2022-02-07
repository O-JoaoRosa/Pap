using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChange : MonoBehaviour
{
    public GameObject car1, car2, car3, car4;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickCar1()
    {
        Vector3 pos  = GameObject.Find("Car(Clone)").transform.position;
        pos.y = pos.y - 4.9f; 
        Destroy(GameObject.Find("Car(Clone)"));
        Instantiate(car1, pos, new Quaternion(0, 0f, 0, 0)).LeanRotate(new Vector3(0, -144f, 0), 0f);
        car1.name = "Car";
        Data.Player.userCarIDSelected = 1;

    }
}
