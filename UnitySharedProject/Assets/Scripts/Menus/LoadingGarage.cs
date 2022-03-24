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
    public GameObject buttonQuit;
    public GameObject buttonLogin;
    public GameObject buttonRegister;
    public GameObject buttonMusic;
    public GameObject buttonLogOut;
    public GameObject buttonBack;

    private bool isInSettings = false;



    AsyncOperation loadingOperation;


    private void Awake()
    {
        buttonBack.SetActive(false);
        buttonMusic.SetActive(false);
        buttonLogOut.SetActive(false);
    }

    private void Update()
    {

        //verifica se o user esta loggado ou nao
        if (Data.Player != null && !isInSettings)
        {
            welcome.GetComponent<Text>().text = "WELCOME " + Data.Player.UserName.ToUpper();
            buttonPlay.SetActive(true);
            buttonSettings.SetActive(true);
            buttonLogin.SetActive(false);
            buttonRegister.SetActive(false);
        }
        else if (Data.Player == null)
        {
            buttonPlay.SetActive(false);
            buttonSettings.SetActive(false);
            buttonLogin.SetActive(true);
            buttonRegister.SetActive(true);
            welcome.GetComponent<Text>().text = "";
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

    public void SetttingsClick()
    {
        isInSettings = true;
        buttonBack.SetActive(true);
        buttonMusic.SetActive(true);
        buttonLogOut.SetActive(true);

        buttonSettings.SetActive(false);
        buttonPlay.SetActive(false);
        buttonQuit.SetActive(false);
    }

    public void BackClick()
    {
        isInSettings = false;
        buttonSettings.SetActive(true);
        buttonPlay.SetActive(true);
        buttonQuit.SetActive(true);

        buttonBack.SetActive(false);
        buttonMusic.SetActive(false);
        buttonLogOut.SetActive(false);
    }

    public void LogOut()
    {
        isInSettings = false;
        Data.Player = null;

        buttonSettings.SetActive(true);
        buttonPlay.SetActive(true);
        buttonQuit.SetActive(true);

        buttonBack.SetActive(false);
        buttonMusic.SetActive(false);
        buttonLogOut.SetActive(false);

        PlayerPrefs.SetString("isRemembered", "false");
        PlayerPrefs.Save();
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
