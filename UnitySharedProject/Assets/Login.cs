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
    WWWForm form;

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
        form = new WWWForm();
        form.AddField("username", UserNameField.text);
        form.AddField("password", PasswordField.text);
        WWW w = new WWW(url, form);
        yield return w;

        if (w.error != null)
        {
            errorMessages.text = "404 not found!";
        }
        else
        {
            if (w.isDone)
            {
                if (w.text.Contains("error"))
                {
                    errorMessages.text = "invalid username or password!";
                }
                else
                {
                    Debug.Log(w.text);
                }
            }
        }

        loginButton.interactable = false;
        loadingIcon.SetActive(false);
        w.Dispose();
    }

    
}
