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
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username=joaofixe&password=joaofixe"));

        form.AddField("username", UserNameField.text);
        form.AddField("password", PasswordField.text);
        UnityWebRequest ww = UnityWebRequest.Post(url, form);



        WWW www = new WWW("https://t05-jrosa.vigion.pt/API/Objects/UserGet.php", form);

        yield return ww.SendWebRequest();
        
        Debug.Log(ww.error);
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
                    string data = ww.downloadHandler.text;

                    Debug.Log(data);
                    jsonData player = JsonUtility.FromJson<jsonData>(data);
                    Debug.Log(player.UserName);
                    Debug.Log(player.money);
                    Debug.Log(player.reputation);
                    Debug.Log(player.ID);
                    Debug.Log(player.userCarIDSelected);
                    
                }
            }
        }

        loginButton.interactable = true;
        loadingIcon.SetActive(false);
        ww.Dispose();
    }

    
}
