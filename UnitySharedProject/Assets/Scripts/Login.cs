using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [Header("Inputs")]
    public InputField UserNameField;
    public InputField PasswordField;

    [Header("Warnings")]
    public Text popUpText;
    public Text errorMessages;
    public GameObject PopUp;

    public GameObject loadingIcon;
    public GameObject MenuPrincipal;

    [Header("Buttons")]
    public Toggle RememberMeButton;
    public Button loginButton;
    string url = "https://t05-jrosa.vigion.pt/API/Objects/UserGet.php";
    public static bool isRemembered = false;


    private void Awake()
    {
        if (PlayerPrefs.GetString("isRemembered") != null || PlayerPrefs.GetString("isRemembered") != "")
        {
            Debug.Log(PlayerPrefs.GetString("isRemembered"));
            loadingIcon.SetActive(false);
            isRemembered = bool.Parse(PlayerPrefs.GetString("isRemembered"));
        }
    }

    public void RememberMe()
    {
        isRemembered = RememberMeButton.isOn;
    }

    public void CancelClic()
    {
        loginButton.interactable = true;
        gameObject.SetActive(false);
        MenuPrincipal.SetActive(true);
    }

    /// <summary>
    /// metodo que vai chamar o login de modo asycrono
    /// </summary>
    public void OnLoginClick()
    {
        loginButton.interactable = false;
        loadingIcon.SetActive(true);
        StartCoroutine(LoginUser());
    }

    /// <summary>
    /// faz login
    /// </summary>
    /// <returns></returns>
    IEnumerator LoginUser()
    {

        if (UserNameField.text == "" || PasswordField.text == "")
        {
            PopUp.SetActive(true);
            popUpText.text = "Password or username invalid";
        }
        else
        {
            Debug.Log(UserNameField.text);

            WWWForm form = new WWWForm();

            //cria um formulario que vai enviar a informação de login
            form.AddField("username", UserNameField.text);
            form.AddField("password", PasswordField.text);

            //cria um web request que vai ligar a api e enviar a informação
            UnityWebRequest ww = UnityWebRequest.Post(url, form);

            //inicia o web request
            yield return ww.SendWebRequest();

            //caso haja erros mostra no debug log
            if (ww.error != null)
            {
                popUpText.text = "404 not found!";
                PopUp.SetActive(true);
                Debug.Log(ww.error);

                //ativa o butao outra vez
                loginButton.interactable = true;
            }
            else
            {
                //verifica se a se os dados foram enviados ou nao
                if (ww.isDone)
                {
                    //verifica se o texto enviado tem algum erro
                    if (ww.downloadHandler.text.Contains("error"))
                    {
                        //informa o user que houve um erro
                        popUpText.text = "invalid username or password!";
                        PopUp.SetActive(true);
                        loginButton.interactable = true;
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

                        Debug.Log(isRemembered);
                        if (isRemembered)
                        {
                            PlayerPrefs.SetString("isRemembered", isRemembered.ToString());
                            PlayerPrefs.SetString("username", UserNameField.text);
                            PlayerPrefs.SetString("password", PasswordField.text);
                            PlayerPrefs.Save();
                        }
                    }
                }
            }

            
            loginButton.interactable = true;
            loadingIcon.SetActive(false);
            ww.Dispose();

            MenuPrincipal.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }

    
}
