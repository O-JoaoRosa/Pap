using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField UserNameField;
    public InputField PasswordField;
    public Text errorMessages;
    public GameObject loadingIcon;
    public GameObject MenuPrincipal;
    public GameObject PopUp;
    public Button loginButton;
    string url = "https://t05-jrosa.vigion.pt/API/Objects/UserGet.php";
    public static bool isRemembered = false;

    public void RememberMe()
    {
        isRemembered = !isRemembered;

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
            errorMessages.text = "404 not found!";
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
                    errorMessages.text = "invalid username or password!";
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
                    loginButton.interactable = false;
                }
            }
        }

        loadingIcon.SetActive(false);
        ww.Dispose();

        MenuPrincipal.SetActive(true);
        gameObject.SetActive(false);
    }

    
}
