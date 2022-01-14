using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MenuRaceSelectionController;

public class MenuController : MonoBehaviour
{
    public GameObject loadingGarage;
    static public string activeTrack;

    
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

    /// <summary>
    /// quando o botão de ready é carregado ele verifica qual pista foi selecionada e faz o loading
    /// </summary>
    public void OkayClick()
    {
        loadingGarage.transform.LeanMoveLocal(new Vector3(0f, -1.25f, 2.2f), 1f).setEaseSpring().setIgnoreTimeScale(true);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(activeTrack);
    }
}
