using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetCars : MonoBehaviour
{
    int i = 0;
    string url = "https://t05-jrosa.vigion.pt/API/Objects/CarGetAll.php";

    private void Start()
    {
        StartCoroutine(GetAllCars());
    }
    
    IEnumerator GetAllCars()
    {
        //cria um web request que vai ligar a api e enviar a informação
        UnityWebRequest ww = UnityWebRequest.Get(url);

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
                //guarda o informação mandada pela api
                string data = ww.downloadHandler.text;
                data = data.Replace("[", "");
                data = data.Replace("]", "");
                data = data.Replace("},{", "}-{");
                string[] test = data.Split('-');

                do
                {
                    CarData car = JsonUtility.FromJson<CarData>(test[i]);
                    Data.cars.Add(car);
                    i++;

                }while (test.Length > i);

                Debug.Log(Data.cars[0]);
                Debug.Log(Data.cars[1]);
                Debug.Log(Data.cars[2]);
                Debug.Log(Data.cars[3]);
                Debug.Log(Data.cars[4]);
            }
        }

        ww.Dispose();
    }
}
