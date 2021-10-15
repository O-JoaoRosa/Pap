using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraScript : MonoBehaviour
{
    GameObject car;
    GameObject textGO;
    Text text;
    Canvas canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();  //vai buscar o primeiro objeto na hierarquia que seja um canvas
        car = GameObject.Find("car_root");           //vai buscar o primeiro objeto que encontrar com o nome dado
        textGO = GameObject.Find("Speed");             //vai buscar o primeiro objeto que encontrar com o nome dado
        text = textGO.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text.text = ((int)car.GetComponent<Rigidbody>().velocity.magnitude).ToString() + " km";
    }
}
