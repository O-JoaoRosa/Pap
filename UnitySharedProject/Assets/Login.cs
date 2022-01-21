using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        form.AddField("UserInfo", UserNameEmailField.text);
        form.AddField("Password", PasswordField.text);

        //envia o formulário para o link escolhido
        WWW url = new WWW("https://t05-jrosa.vigion.pt/API/Objects/UserGet.php", form);
        yield return url;
        if (url.text[0] == '0')
        {
            Debug.Log("Feito !");
            gameObject.SetActive(false);
            MenuPrincipal.SetActive(true);
        }
        else
        {
            Debug.Log("ERRO: " + url.text);
            PopUp.SetActive(true);

        }
    }
}
