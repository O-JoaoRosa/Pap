using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Register : MonoBehaviour
{
    public InputField nameField, emailField, passwordField;

    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(RegisterUser());
    }

    IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("UserName", nameField.text);
        form.AddField("Password", passwordField.text);
        form.AddField("Email", emailField.text);

        WWW url = new WWW("link" , form);
        yield return url;
        if (url.text == "0")
        {
            Debug.Log("Feito !");
        }
    }

}
