using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CarChange : MonoBehaviour
{
    public GameObject car1, car2, car3, car4;
    public GameObject player;
    string url = "https://t05-jrosa.vigion.pt/API/Objects/CarGetAll.php";


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
        Data.Player.UserCarIDSelected = 1;

    }


    public void onClickCar2()
    {
        Vector3 pos  = GameObject.Find("Car(Clone)").transform.position;
        pos.y = pos.y - 4.9f; 
        Destroy(GameObject.Find("Car(Clone)"));
        Instantiate(car2, pos, new Quaternion(0, 0f, 0, 0)).LeanRotate(new Vector3(0, -144f, 0), 0f);
        car2.name = "Car";
        Data.Player.UserCarIDSelected = 2;

    }

    public void onClickCar3()
    {
        Vector3 pos  = GameObject.Find("Car(Clone)").transform.position;
        pos.y = pos.y - 4.9f; 
        Destroy(GameObject.Find("Car(Clone)"));
        Instantiate(car3, pos, new Quaternion(0, 0f, 0, 0)).LeanRotate(new Vector3(0, -144f, 0), 0f);
        car3.name = "Car";
        Data.Player.UserCarIDSelected = 3;

    }

    public void onClickCar4()
    {
        Vector3 pos  = GameObject.Find("Car(Clone)").transform.position;
        pos.y = pos.y - 4.9f; 
        Destroy(GameObject.Find("Car(Clone)"));
        Instantiate(car4, pos, new Quaternion(0, 0f, 0, 0)).LeanRotate(new Vector3(0, -144f, 0), 0f);
        car4.name = "Car";
        Data.Player.UserCarIDSelected = 0;

    }


    public void UpdateCar()
    {
        StartCoroutine(UpdateCarSelected());
    }

    IEnumerator UpdateCarSelected()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID",Data.Player.ID);
        form.AddField("UserName",Data.Player.UserName);
        form.AddField("Money", Data.Player.money);
        form.AddField("Reputation",Data.Player.reputation);
        form.AddField("UserCarIDSelected",Data.Player.UserCarIDSelected);

        //cria um web request que vai ligar a api e enviar a informação
        UnityWebRequest ww = UnityWebRequest.Post(url, form);

        //inicia o web request
        yield return ww.SendWebRequest();

        //caso haja erros mostra no debug log
        if (ww.error != null)
        {
            Debug.LogError(ww.error);
        }
        else
        {
            if (ww.isDone)
            {
                if (ww.downloadHandler.text.Contains("sucesso"))
                {
                    //TODO : Pop up a confirmar que funcionou
                }
            }
        }
    }
}
