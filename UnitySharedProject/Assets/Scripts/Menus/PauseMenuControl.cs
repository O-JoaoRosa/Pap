using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControl : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //if the escape button is pressed it shows the menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                pauseMenu.SetActive(false);
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
            }
        }
    }
}
