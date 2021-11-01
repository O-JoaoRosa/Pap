using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGarage : MonoBehaviour
{
    public Animator anim;
    AsyncOperation loadingOperation;
    public Slider progressBar;
    public GameObject loadingText;

    private void Awake()
    {
        loadingText.SetActive(false);
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
        loadingText.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        loadingOperation = SceneManager.LoadSceneAsync("TestWorld", LoadSceneMode.Single);
    }
    void Update()
    {
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }
}
