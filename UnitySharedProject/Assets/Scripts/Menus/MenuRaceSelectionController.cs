using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MenuController;

public class MenuRaceSelectionController : MonoBehaviour
{
    private string track1 = "TestWorld";
    private string track2 = "tutorialRace";

    [Header("imagens")]
    public RawImage trackImg;

    [Header("Textures")]
    public Texture texture1;
    public Texture texture2;
    public Texture texture3;
    public Texture texture4;
    public Texture texture5;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


    /// <summary>
    /// muda as pistas dependendo do but�o selecionado
    /// </summary>
    public void ButtonTrack1Click()
    {
        trackImg.texture = texture1;
        activeTrack = track1;
    }

    public void ButtonTrack2Click()
    {
        trackImg.texture = texture2;
        activeTrack = track2;
    }

    public void ButtonTrack3Click()
    {
        trackImg.texture = texture3;
        activeTrack = track1;
    }

    public void ButtonTrack4Click()
    {
        trackImg.texture = texture4;
        activeTrack = track1;
    }

    public void ButtonTrack5Click()
    {
        trackImg.texture = texture5;
        activeTrack = track1;
    }
}