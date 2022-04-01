using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MenuController;

public class MenuRaceSelectionController : MonoBehaviour
{
    private string track1 = "tutorialRace";
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
    /// muda as pistas dependendo do butão selecionado
    /// </summary>
    public void ButtonTrack1Click()
    {
        Data.Track.TrackName = "Noob Shredder";
        trackImg.texture = texture1;
        activeTrack = track1;
    }

    public void ButtonTrack2Click()
    {
        Data.Track.TrackName = "Noob Shredder";
        trackImg.texture = texture2;
        activeTrack = track2;
    }

    public void ButtonTrack3Click()
    {
        Data.Track.TrackName = "Track 3";
        trackImg.texture = texture3;
        activeTrack = track1;
    }

    public void ButtonTrack4Click()
    {
        Data.Track.TrackName = "Track 4";
        trackImg.texture = texture4;
        activeTrack = track1;
    }

    public void ButtonTrack5Click()
    {
        Data.Track.TrackName = "Track 5";
        trackImg.texture = texture5;
        activeTrack = track1;
    }
}
