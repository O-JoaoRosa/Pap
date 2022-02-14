using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AccountControll : MonoBehaviour
{

    public GameObject RegisterMenu;
    public GameObject LoginMenu;
    public GameObject MainMenu;

    //verções static dos game objects 
    private static GameObject RegisterMenuSt;
    private static GameObject LoginMenuSt;
    private static GameObject MainMenuSt;

    string url = "https://t05-jrosa.vigion.pt/API/Objects/UserGet.php";
    string username;
    string password;

    // Start is called before the first frame update
    void Awake()
    {
        RegisterMenuSt = RegisterMenu;
        LoginMenuSt = LoginMenu;
        MainMenuSt = MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// muda de menu caso o registo tenha tido sucesso 
    /// </summary>
    public static void RegistrationComplete()
    {
        MainMenuSt.SetActive(true);
        LoginMenuSt.SetActive(false);
        RegisterMenuSt.SetActive(false);
    }

    public void ActivateMenuLogin()
    {
        if (Login.isRemembered)
        {
            Debug.Log("Account Remembered");
            Debug.Log("getting data from player prefs");

            username = PlayerPrefs.GetString("username");
            Debug.Log(username);

            password = PlayerPrefs.GetString("password");
            Debug.Log(password);

            StartCoroutine(rememberedLogin());
           
        }
        else
        {
            RegisterMenu.SetActive(false);
            LoginMenu.SetActive(true);
            MainMenu.SetActive(false);
        }
    }


    public void ActivateMenuRegister()
    {
        LoginMenu.SetActive(false);
        RegisterMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    IEnumerator rememberedLogin()
    {
        Debug.Log("username: " + username);
        Debug.Log("password: " + password);

        WWWForm form = new WWWForm();

        //cria um formulario que vai enviar a informação de login
        form.AddField("username", username);
        form.AddField("password", password);

        //cria um web request que vai ligar a api e enviar a informação
        UnityWebRequest ww = UnityWebRequest.Post(url, form);

        //inicia o web request
        yield return ww.SendWebRequest();

        //caso haja erros mostra no debug log
        if (ww.error != null)
        {
            Debug.Log(ww.error);
        }
        else
        {
            //verifica se a se os dados foram enviados ou nao
            if (ww.isDone)
            {
                //verifica se o texto enviado tem algum erro
                if (ww.downloadHandler.text.Contains("error"))
                {
                    ////informa o user que houve um erro
                    //popUpText.text = "invalid username or password!";
                    //PopUp.SetActive(true);
                }
                else
                {
                    Debug.Log(ww.downloadHandler.text);

                    //guarda o informação mandada pela api
                    string data = ww.downloadHandler.text;
                    data = data.Replace("[", "");
                    data = data.Replace("]", "");

                    //converte a informação recebida da api para um player
                    jsonData player = JsonUtility.FromJson<jsonData>(data);

                    //mostar os dados do jogador
                    Debug.Log(player.UserName);
                    Debug.Log(player.money);
                    Debug.Log(player.reputation);
                    Debug.Log(player.ID);
                    Debug.Log(player.UserCarIDSelected);

                    //passa o player para um objecto que vai manter os dados guardados
                    Data.Player = player;
                }
            }
        }
        ww.Dispose();
    }
}
