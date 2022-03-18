using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CarChange : MonoBehaviour
{
    public GameObject car1Prefab, car2Prefab, car3Prefab, car4Prefab;
    private GameObject car1, car2, car3, car4;
    [Header("Stars")]
    public GameObject Star1, Star2, Star3, Star4;
    public GameObject FirstMenuStar1, FirstMenuStar2, FirstMenuStar3, FirstMenuStar4;
    public GameObject player;
    Vector3 pos;
    string url = "https://t05-jrosa.vigion.pt/API/Objects/UserCommonUpdate.php";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRarity();
    }

    /// <summary>
    /// muda a quantidade de estrelas que o carro tem no menu
    /// </summary>
    void ChangeRarity()
    {
        switch (Data.ActiveCar.Rarity)
        {
            case 1:

                //muda as estrelas do segundo menu
                Star1.GetComponent<Image>().color = Color.white;
                Star2.GetComponent<Image>().color = Color.gray;
                Star3.GetComponent<Image>().color = Color.gray;
                Star4.GetComponent<Image>().color = Color.gray;

                //muda as estrelas do primeiro menu
                FirstMenuStar1.GetComponent<Image>().color = Color.white;
                FirstMenuStar2.GetComponent<Image>().color = Color.gray;
                FirstMenuStar3.GetComponent<Image>().color = Color.gray;
                FirstMenuStar4.GetComponent<Image>().color = Color.gray;

                break;

            case 2:

                //muda as estrelas do segundo menu
                Star1.GetComponent<Image>().color = Color.white;
                Star2.GetComponent<Image>().color = Color.white;
                Star3.GetComponent<Image>().color = Color.gray;
                Star4.GetComponent<Image>().color = Color.gray;

                //muda as estrelas do primeiro menu
                FirstMenuStar1.GetComponent<Image>().color = Color.white;
                FirstMenuStar2.GetComponent<Image>().color = Color.white;
                FirstMenuStar3.GetComponent<Image>().color = Color.gray;
                FirstMenuStar4.GetComponent<Image>().color = Color.gray;

                break;

            case 3:

                //muda as estrelas do segundo menu
                Star1.GetComponent<Image>().color = Color.white;
                Star2.GetComponent<Image>().color = Color.white;
                Star3.GetComponent<Image>().color = Color.white;
                Star4.GetComponent<Image>().color = Color.gray;

                //muda as estrelas do primeiro menu
                FirstMenuStar1.GetComponent<Image>().color = Color.white;
                FirstMenuStar2.GetComponent<Image>().color = Color.white;
                FirstMenuStar3.GetComponent<Image>().color = Color.white;
                FirstMenuStar4.GetComponent<Image>().color = Color.gray;

                break;

            case 4:

                //muda as estrelas do segundo menu
                Star1.GetComponent<Image>().color = Color.white;
                Star2.GetComponent<Image>().color = Color.white;
                Star3.GetComponent<Image>().color = Color.white;
                Star4.GetComponent<Image>().color = Color.white;

                //muda as estrelas do primeiro menu
                FirstMenuStar1.GetComponent<Image>().color = Color.white;
                FirstMenuStar2.GetComponent<Image>().color = Color.white;
                FirstMenuStar3.GetComponent<Image>().color = Color.white;
                FirstMenuStar4.GetComponent<Image>().color = Color.white;

                break;
        }
    }

    #region Clicks

    public void onClickCar1()
    {

        Data.Player.UserCarIDSelected = 0;
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

        Data.Player.UserCarIDSelected = 0;
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

        Data.Player.UserCarIDSelected = 0;
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

    #endregion

    public void UpdateCar()
    {
        Debug.Log("Starting Corutine");
        StartCoroutine(UpdateCarSelected());
        gameObject.SetActive(false);
        Debug.Log("Executando o evento CarSelectorExit()");
    }

    IEnumerator UpdateCarSelected()
    {
        Debug.Log("Making The web form");
        WWWForm form = new WWWForm();
        form.AddField("ID",Data.Player.ID);
        form.AddField("UserName",Data.Player.UserName);
        form.AddField("Money", Data.Player.Money);
        form.AddField("Reputation",Data.Player.Reputation);
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
                Debug.Log("feito");
                Debug.Log(ww.downloadHandler.text);
                if (ww.downloadHandler.text.Contains("sucesso"))
                {
                    Debug.Log(ww.downloadHandler.text);
                    Debug.Log("feito");
                    //TODO : Pop up a confirmar que funcionou
                }
            }
        }
        ww.Dispose();
    }
}
