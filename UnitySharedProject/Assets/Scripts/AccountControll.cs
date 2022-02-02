using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountControll : MonoBehaviour
{

    public GameObject RegisterMenu;
    public GameObject LoginMenu;
    public GameObject MainMenu;

    //verções static dos game objects 
    private static GameObject RegisterMenuSt;
    private static GameObject LoginMenuSt;
    private static GameObject MainMenuSt;
    private static GameObject ThisMenu;


    // Start is called before the first frame update
    void Awake()
    {
        ThisMenu = gameObject;
        gameObject.SetActive(false);
        RegisterMenuSt = RegisterMenu;
        LoginMenuSt = LoginMenu;
        MainMenuSt = MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// muda de menu caso o registo tenha tido sucesso 
    /// </summary>
    public static void RegistrationComplete()
    {
        ThisMenu.SetActive(false);
        MainMenuSt.SetActive(true);
        LoginMenuSt.SetActive(false);
        RegisterMenuSt.SetActive(false);
    }

    public void ActivateMenuLogin()
    {
        gameObject.SetActive(true);
        RegisterMenu.SetActive(false);
        LoginMenu.SetActive(true);
        MainMenu.SetActive(false);
    }


    public void ActivateMenuRegister()
    {
        gameObject.SetActive(true);
        LoginMenu.SetActive(false);
        RegisterMenu.SetActive(true);
        MainMenu.SetActive(false);
    }
}
