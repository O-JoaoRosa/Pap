using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Register : MonoBehaviour
{
    [Header("Inputs")]
    public InputField nameField;
    public InputField emailField;
    public InputField passwordField;
    public InputField confirmPasswordField;

    [Header("Menus")]
    public GameObject MenuLogin;
    public GameObject MenuPrincipal;


    [Header("Ckecks")]
    public GameObject CheckName;
    public GameObject CheckEmail;
    public GameObject CheckPasword;
    public GameObject CheckComfirmPassword;

    [Header("Loading")]
    public GameObject loading;
    public GameObject loadingImage;

    [Header("Button")]
    public UnityEngine.UI.Button submitButton;

    private bool nameIsOk = false, emailIsOk = false, passwordIsOk = false, isLoading = false;

    private void Awake()
    {
        submitButton.interactable = false;
        loading.SetActive(false);
        CheckComfirmPassword.SetActive(false);
        CheckEmail.SetActive(false);
        CheckName.SetActive(false);
        CheckPasword.SetActive(false);
    }

    public void CallRegister()
    {
        StartCoroutine(RegisterUser());
        gameObject.SetActive(false);
        MenuLogin.SetActive(false);
        MenuPrincipal.SetActive(true);

    }

    public void Cancel()
    {
        gameObject.SetActive(false);
        MenuPrincipal.SetActive(true);
    }

    public void Update()
    {
        CheckRegister();
        StartLoading();
    }

    IEnumerator RegisterUser()
    {
        isLoading = true;

        //cria um formolário para enviar para a api
        WWWForm form = new WWWForm();
        form.AddField("UserName", nameField.text);
        form.AddField("Password", passwordField.text);
        form.AddField("Email", emailField.text);

        //envia o formulário para o link escolhido
        WWW url = new WWW("https://t05-jrosa.vigion.pt/API/Objects/UserAdd.php", form);
        yield return url;
        if (url.text == "0")
        {
            Debug.Log("Feito !");
            gameObject.SetActive(false);
            MenuLogin.SetActive(false);
            MenuPrincipal.SetActive(true);
        }
        else
        {
            Debug.Log("ERRO: " + url.text);
        }

        isLoading = false;
    }


    public void StartLoading()
    {
        if (isLoading)
        {
            loading.SetActive(true);
            loadingImage.transform.LeanRotate(new Vector3(0, 0, 360f), 3f).callOnCompletes();
        }
        else
        {
            loading.SetActive(false);
        }
    }

    /// <summary>
    /// verifica se pode registar o user
    /// </summary>
    private void CheckRegister()
    {
        //verifica o tamanho do nome
        if ((nameField.text.Length > 4 && nameField.text.Length < 16))
        {
            CheckName.SetActive(true);
            nameIsOk = true;
            nameField.selectionColor = Color.green;
        }
        else
        {
            CheckName.SetActive(false);
            nameIsOk = false;
            nameField.selectionColor = Color.red;
        }

        if (passwordField.text.Length > 5)
        {
            CheckPasword.SetActive(true);

            //verifica se a palavra pass é igual à sua confirmação
            if (passwordField.text == confirmPasswordField.text)
            {
                CheckComfirmPassword.SetActive(true);
                passwordIsOk = true;
                passwordField.selectionColor = Color.green;
                confirmPasswordField.selectionColor = Color.green;
            }
            else
            {
                CheckComfirmPassword.SetActive(false);
                passwordIsOk = false;
                confirmPasswordField.selectionColor = Color.red;
            }
        }
        else
        {
            CheckPasword.SetActive(false);
        }

        //verifica se o emial tem o necessario para ser um email
        if (emailField.text.Contains("@") && (emailField.text.Contains(".com") || emailField.text.Contains(".pt")))
        {
            CheckEmail.SetActive(true);
            emailIsOk = true;
            emailField.selectionColor = Color.green;
        }
        else
        {
            CheckEmail.SetActive(false);
            emailIsOk = false;
            emailField.selectionColor = Color.red;
        }


        //verifica se todas as verificações estão correctas e permite ou não registar o user
        if (nameIsOk && passwordIsOk && emailIsOk)
        {
            submitButton.interactable = true;
        }
        else
        {
            submitButton.interactable = false;
        }
    }

}
