using UnityEngine;

public class GameSelectOnline : MonoBehaviour
{
    [Header("Menus")]
    public GameObject menuBackground;

    [Header("Offline")]
    public GameObject OfflineOption1;
    public GameObject OfflineOption2;
    public GameObject OfflineOption3;

    [Header("Online")]
    public GameObject OnlineOption1;
    public GameObject OnlineOption2;
    public GameObject OnlineOption3;
    public GameObject PartyList;

    private bool areOnlineOptionsHidden = true;
    private bool areOfflineOptionsHidden = true;

    public void ButtonNextClick()
    {
        menuBackground.transform.LeanMoveLocal(new Vector3(0, 0, 10f), 0.4f).setEaseInQuart();
        menuBackground.transform.LeanMoveLocal(new Vector3(-2467, 0, 10f), 1f).setEaseInQuart().setDelay(0.5f);
        menuBackground.transform.LeanMoveLocal(new Vector3(-2467, 0, 0f), 0.4f).setEaseInQuart().setDelay(1.6f);
    }
    
    public void ButtonBackClick()
    {
        menuBackground.transform.LeanMoveLocal(new Vector3(-2467, 0, 10f), 0.4f).setEaseInQuart();
        menuBackground.transform.LeanMoveLocal(new Vector3(0, 0, 10f), 1f).setEaseInQuart().setDelay(0.5f);
        menuBackground.transform.LeanMoveLocal(new Vector3(0, 0, 0f), 0.4f).setEaseInQuart().setDelay(1.6f);
    }

    /// <summary>
    /// metodo que é executado quando o butao é carregado
    /// </summary>
    public void ButtonOnlineClick()
    {
        //verifica se a as opções estão a mostra ou nao 
        if (areOnlineOptionsHidden)
        {
            OnlineOption1.transform.LeanMoveLocal(new Vector3(-83f, 30f, 0), 0.5f).setEaseSpring();
            OnlineOption2.transform.LeanMoveLocal(new Vector3(-53.7f, 39f, 0), 0.5f).setEaseSpring();
            OnlineOption3.transform.LeanMoveLocal(new Vector3(-22f, 30f, 0), 0.5f).setEaseSpring();
            areOnlineOptionsHidden = false;
        }
        else
        {
            OnlineOption1.transform.LeanMoveLocal(new Vector3(-67f, 4.3f, 0), 0.45f).setEaseInBack();
            OnlineOption2.transform.LeanMoveLocal(new Vector3(-53.7f, 4.3f, 0), 0.45f).setEaseInBack();
            OnlineOption3.transform.LeanMoveLocal(new Vector3(-40f, 4.3f, 0), 0.45f).setEaseInBack();
            areOnlineOptionsHidden = true;
        }
    }

    /// <summary>
    /// metodo que é executado quando o butao é carregado
    /// </summary>
    public void ButtonOfflineClick()
    {
        //verifica se a as opções estão a mostra ou nao 
        if (areOfflineOptionsHidden)
        {
            OfflineOption1.transform.LeanMoveLocal(new Vector3(30f, -30f, 0), 0.5f).setEaseSpring();
            OfflineOption2.transform.LeanMoveLocal(new Vector3(59f, -41.6f, 0), 0.5f).setEaseSpring();
            OfflineOption3.transform.LeanMoveLocal(new Vector3(89f, -30f, 0), 0.5f).setEaseSpring();
            areOfflineOptionsHidden = false;
        }
        else
        {
            OfflineOption1.transform.LeanMoveLocal(new Vector3(44.8f, -4.5f, 0), 0.45f).setEaseInBack();
            OfflineOption2.transform.LeanMoveLocal(new Vector3(59f, -4.5f, 0), 0.45f).setEaseInBack();
            OfflineOption3.transform.LeanMoveLocal(new Vector3(73f, -4.5f, 0), 0.45f).setEaseInBack();
            areOfflineOptionsHidden = true;
        }
    }
}
