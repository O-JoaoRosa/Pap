using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGarage : MonoBehaviour
{
    public Animator anim;
    AsyncOperation loadingOperation;

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
