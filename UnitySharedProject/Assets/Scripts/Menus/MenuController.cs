using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private string trackOne = "TestWorld";
    private string track2 = "tutorialRace";
    public GameObject loadingGarage;
    public string activeTrack;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //verifica se o botão esc foi carregado para sair da pausa
        if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void OkayClick()
    {
        loadingGarage.transform.LeanMoveLocal(new Vector3(0f, -1.25f, 2.2f), 1f).setEaseSpring().setIgnoreTimeScale(true);
        SceneManager.LoadSceneAsync(activeTrack);

    }
}
