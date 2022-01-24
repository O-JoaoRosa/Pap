using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGarage : MonoBehaviour
{
    public Animator anim;
    public GameObject welcome;
    
    [Header("Buttons")]
    public GameObject buttonPlay;
    public GameObject buttonSettings;
    public GameObject buttonLogin;
    public GameObject buttonRegister;

    AsyncOperation loadingOperation;

    private void Update()
    {
        //verifica se o user esta loggado ou nao
        if (DBManager.loggedIn)
        {
            welcome.GetComponent<Text>().text = "Welcome " + DBManager.username;
            buttonPlay.SetActive(true);
            buttonSettings.SetActive(true);
            buttonLogin.SetActive(false);
            buttonRegister.SetActive(false);
        }
        else
        {
            buttonPlay.SetActive(false);
            buttonSettings.SetActive(false);
            buttonLogin.SetActive(true);
            buttonRegister.SetActive(true);
            welcome.GetComponent<Text>().text = "Welcome please login or register";
        }
    }

    //closes the game
    public void ExitGame()
    {
        Application.Quit();
    }

    //loads the main scene
    public void StartGame()
    {
        StartCoroutine(loadingAnimation());
    }

    //metod used to play the animations before loading the game
    private IEnumerator loadingAnimation()
    {
        //indica que animação iniciar
        anim.SetBool("isDefault", false);
        anim.SetBool("IsLoading", true);

        //faz o script esperar por 1 segundo
        yield return new WaitForSecondsRealtime(1);

        //mostra o texto de loading e carrega cena
        yield return new WaitForSecondsRealtime(1);
        loadingOperation = SceneManager.LoadSceneAsync("GarageLobby", LoadSceneMode.Single);
    }
}
