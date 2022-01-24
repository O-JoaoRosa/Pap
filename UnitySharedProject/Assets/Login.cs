using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField UserNameEmailField;
    public InputField PasswordField;
    public GameObject MenuPrincipal;
    public GameObject PopUp;

    public void CallLogin()
    {
        StartCoroutine(LoginUser());
    }

    IEnumerator LoginUser()
    {
        //cria um formolário para enviar para a api
        WWWForm form = new WWWForm();

        UnityWebRequest www = new UnityWebRequest("https://t05-jrosa.vigion.pt/API/Objects/UserGet.php");
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("UserInfo", UserNameEmailField.text));
        formData.Add(new MultipartFormDataSection("Password", PasswordField.text));

        www = UnityWebRequest.Post("https://t05-jrosa.vigion.pt/API/Objects/UserGet.php", formData);
        yield return www.SendWebRequest();

        www = UnityWebRequest.Get("https://t05-jrosa.vigion.pt/API/Objects/UserGet.php");
        yield return www.SendWebRequest();


        //form.AddField("UserInfo", UserNameEmailField.text);
        //form.AddField("Password", PasswordField.text);

        ////envia o formulário para o link escolhido
        //WWW url = new WWW("https://t05-jrosa.vigion.pt/API/Objects/UserGet.php", form);

        //yield return url;

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Feito !");
            DBManager.username = UserNameEmailField.text;
            DBManager.username = www.downloadHandler.text.Split('\t')[1];
            DBManager.userEmail= www.downloadHandler.text.Split('\t')[2];
            DBManager.Money = int.Parse(www.downloadHandler.text.Split('\t')[3]);
            DBManager.Reputation = int.Parse(www.downloadHandler.text.Split('\t')[4]);
            DBManager.userCarIdSelected = int.Parse(www.downloadHandler.text.Split('\t')[5]);
        }
        else
        {
            Debug.Log("ERRO: " + www.downloadHandler.text);
            PopUp.SetActive(true);

        }
    }
}
