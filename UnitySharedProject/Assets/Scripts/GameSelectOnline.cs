using UnityEngine;

public class GameSelectOnline : MonoBehaviour
{
    [Header("Menus")]
    public GameObject menuBackground;
    public GameObject menuAreYouReady;

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

    /// <summary>
    /// metodo executado ao carregar no botão next para executar as animações
    /// </summary>
    public void ButtonNextClick()
    {
        //sequencia de animações que vai mudar de menu
        menuBackground.transform.LeanMoveLocal(new Vector3(0, 0, 20f), 0.5f).setEaseInBack().setIgnoreTimeScale(true);
        menuBackground.transform.LeanMoveLocal(new Vector3(-2467, 0, 20f), 1.2f).setEaseInBack().setIgnoreTimeScale(true).setDelay(0.6f);
        menuBackground.transform.LeanMoveLocal(new Vector3(-2467, 0, 0f), 0.5f).setEaseInBack().setIgnoreTimeScale(true).setDelay(1.9f);
    }

    /// <summary>
    /// metodo executado ao carregar no botão back para executar as animações
    /// </summary>
    public void ButtonBackClick()
    {
        //sequencia de animações que vai mudar de menu
        menuBackground.transform.LeanMoveLocal(new Vector3(-2467, 0, 20f), 0.5f).setEaseInBack().setIgnoreTimeScale(true);
        menuBackground.transform.LeanMoveLocal(new Vector3(0, 0, 20f), 1.2f).setEaseInBack().setIgnoreTimeScale(true).setDelay(0.6f);
        menuBackground.transform.LeanMoveLocal(new Vector3(0, 0, 0f), 0.5f).setEaseInBack().setIgnoreTimeScale(true).setDelay(1.9f);
    }

    /// <summary>
    /// metodo que é executado quando o butao é carregado
    /// </summary>
    public void ButtonOnlineClick()
    {
        //verifica se a as opções estão a mostra ou nao 
        if (areOnlineOptionsHidden)
        {
            //executa as animações dos butões online
            OnlineOption1.transform.LeanMoveLocal(new Vector3(-83f, 30f, 0), 0.5f).setEaseSpring().setIgnoreTimeScale(true);
            OnlineOption2.transform.LeanMoveLocal(new Vector3(-53.7f, 39f, 0), 0.5f).setEaseSpring().setIgnoreTimeScale(true);
            OnlineOption3.transform.LeanMoveLocal(new Vector3(-22f, 30f, 0), 0.5f).setEaseSpring().setIgnoreTimeScale(true);
            areOnlineOptionsHidden = false;
        }
        else
        {
            //esconde os botões caso eles estejam a mostra
            OnlineOption1.transform.LeanMoveLocal(new Vector3(-67f, 4.3f, 0), 0.45f).setEaseInBack().setIgnoreTimeScale(true);
            OnlineOption2.transform.LeanMoveLocal(new Vector3(-53.7f, 4.3f, 0), 0.45f).setEaseInBack().setIgnoreTimeScale(true);
            OnlineOption3.transform.LeanMoveLocal(new Vector3(-40f, 4.3f, 0), 0.45f).setEaseInBack().setIgnoreTimeScale(true);
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
            //faz as animações para mostrar as opções
            OfflineOption1.transform.LeanMoveLocal(new Vector3(30f, -30f, 0), 0.5f).setIgnoreTimeScale(true).setEaseSpring();
            OfflineOption2.transform.LeanMoveLocal(new Vector3(59f, -41.6f, 0), 0.5f).setIgnoreTimeScale(true).setEaseSpring();
            OfflineOption3.transform.LeanMoveLocal(new Vector3(89f, -30f, 0), 0.5f).setIgnoreTimeScale(true).setEaseSpring();
            areOfflineOptionsHidden = false;
        }
        else
        {
            //esconde as opções 
            OfflineOption1.transform.LeanMoveLocal(new Vector3(44.8f, -4.5f, 0), 0.45f).setIgnoreTimeScale(true).setEaseInBack();
            OfflineOption2.transform.LeanMoveLocal(new Vector3(59f, -4.5f, 0), 0.45f).setIgnoreTimeScale(true).setEaseInBack();
            OfflineOption3.transform.LeanMoveLocal(new Vector3(73f, -4.5f, 0), 0.45f).setIgnoreTimeScale(true).setEaseInBack();
            areOfflineOptionsHidden = true;
        }
    }

    /// <summary>
    /// botão que vai decidir oq vai acontecer
    /// </summary>
    public void ButtonOnlineOption1Pressed()
    {
        menuAreYouReady.SetActive(true);
        gameObject.SetActive(false);

    }
    
    /// <summary>
    /// botão que vai decidir oq vai acontecer
    /// </summary>
    public void ButtonOnlineOption2Pressed()
    {
        menuAreYouReady.SetActive(true);
        gameObject.SetActive(false);

    }
    
    /// <summary>
    /// botão que vai decidir oq vai acontecer
    /// </summary>
    public void ButtonOnlineOption3Pressed()
    {
        menuAreYouReady.SetActive(true);
        gameObject.SetActive(false);

    }

    /// <summary>
    /// botão que vai decidir oq vai acontecer
    /// </summary>
    public void ButtonOfflineOption1Pressed()
    {
        menuAreYouReady.SetActive(true);
        gameObject.SetActive(false);

    }
    
    /// <summary>
    /// botão que vai decidir oq vai acontecer
    /// </summary>
    public void ButtonOfflineOption2Pressed()
    {
        menuAreYouReady.SetActive(true);
        gameObject.SetActive(false);

    }
    
    /// <summary>
    /// botão que vai decidir oq vai acontecer
    /// </summary>
    public void ButtonOfflineOption3Pressed()
    {
        menuAreYouReady.SetActive(true);
        gameObject.SetActive(false);

    }
}
