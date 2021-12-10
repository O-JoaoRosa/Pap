using UnityEngine;

public class GameSelectOnline : MonoBehaviour
{
    private bool areOnlineOptionsHidden = true;
    private bool areOfflineOptionsHidden = true;

    /// <summary>
    /// metodo que é executado quando o butao é carregado
    /// </summary>
    public void ButtonOnlineClick()
    {
        //verifica se a as opções estão a mostra ou nao 
        if (areOnlineOptionsHidden)
        {
            areOnlineOptionsHidden = false;
        }
        else
        {
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
            LeanTween.spring(0f, 10f, 20f);
            areOfflineOptionsHidden = false;
        }
        else
        {

            areOfflineOptionsHidden = true;
        }
    }
}
