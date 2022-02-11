using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CarChange : MonoBehaviour
{
    public GameObject car1Prefab, car2Prefab, car3Prefab, car4Prefab;
    private GameObject car1, car2, car3, car4;
    public GameObject player;
    Vector3 pos;
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
        car1 = Instantiate(car1Prefab, pos, new Quaternion(0, 0f, 0, 0));
        car1.LeanRotate(new Vector3(0, -144f, 0), 0f);
        car1.name = "Car";
        Data.Player.UserCarIDSelected = 1;

    }


    public void onClickCar2()
    {
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
        car2 = Instantiate(car2Prefab, pos, new Quaternion(0, 0f, 0, 0));
        car2.LeanRotate(new Vector3(0, -144f, 0), 0f);
        car2.name = "Car";
        Data.Player.UserCarIDSelected = 2;

    }

    public void onClickCar3()
    {
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

    }

    public void onClickCar4()
    {
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
        car4 = Instantiate(car4Prefab, pos, new Quaternion(0, 0f, 0, 0));
        car4.LeanRotate(new Vector3(0, -144f, 0), 0f);
        car4.name = "Car";
        Data.Player.UserCarIDSelected = 0;

    }


    public void UpdateCar()
    {
        StartCoroutine(UpdateCarSelected());
        EventController.current.CarSelectorExit();
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
        Debug.Log(ww.downloadHandler.text);

        //caso haja erros mostra no debug log
        if (ww.error != null)
        {
            Debug.Log(ww.downloadHandler.text);
            Debug.LogError(ww.error);
        }
        else
        {
            if (ww.isDone)
            {
                if (ww.downloadHandler.text.Contains("sucesso"))
                {
                    Debug.Log(ww.downloadHandler.text);
                    //TODO : Pop up a confirmar que funcionou
                }
            }
        }
        ww.Dispose();
    }
}
