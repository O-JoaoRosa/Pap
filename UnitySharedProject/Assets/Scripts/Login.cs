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
        loginButton.interactable = true;
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
        UnityWebRequest ww = new UnityWebRequest(url, "GET");


        WWW w = new WWW(url, form);
        yield return w;
        
        Debug.Log(ww);
        if (ww.error != null)
        {
            errorMessages.text = "404 not found!";
            Debug.Log(ww.error);
        }
        else
        {
            Debug.Log(ww.downloadHandler.text);
            if (ww.isDone)
            {
                Debug.Log(ww.downloadHandler.text);
                if (ww.downloadHandler.text.Contains("error"))
                {
                    Debug.Log(ww.downloadHandler.text);
                    errorMessages.text = "invalid username or password!";
                }
                else
                {
                    Debug.Log(ww.downloadHandler.text);
                    jsonData jsn = JsonUtility.FromJson<jsonData>(ww.downloadHandler.text);
                    Debug.Log(jsn.UserName);
                    Debug.Log(jsn.money);
                    Debug.Log(jsn.reputation);
                    Debug.Log(jsn.ID);
                    Debug.Log(jsn.userCarIDSelected);
                }
            }
        }

        loginButton.interactable = true;
        loadingIcon.SetActive(false);
        w.Dispose();
    }

    
}
