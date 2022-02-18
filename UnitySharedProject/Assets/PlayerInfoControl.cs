using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoControl : MonoBehaviour
{
    [Header("UI Elements")]
    public Text PlayerName;
    public Text MoneyValue;
    public Slider LevelSlider;
    public Image LevelSliderImage;
    public RawImage LevelNumber;
    public Image LevelFrame;

    [Header("Textures Numbers")]
    public Texture Number1;
    public Texture Number2;
    public Texture Number3;
    public Texture Number4;
    public Texture Number5;
    public Texture Number6;
    public Texture Number7;
    public Texture Number8;
    public Texture Number9;

    [Header("Textures LevelFrames")]
    public Sprite SliderType1;
    public Sprite SliderType2;
    public Sprite SliderType3;
    public Sprite SliderType4;

    public Sprite LevelType1;
    public Sprite LevelType2;
    public Sprite LevelType3;
    public Sprite LevelType4;



    // Start is called before the first frame update
    void Start()
    {
        PlayerName.text = Data.Player.UserName.ToUpper();
        MoneyValue.text = Data.Player.Money.ToString();
        
        #region Level Limits & Frame Changes

        //verifica o level do player e muda o nivel e o maximo do nivel do player
        if (Data.Player.Reputation >= 500 && Data.Player.Reputation < 1500)
        {
            LevelSlider.minValue = 500;
            LevelSlider.maxValue = 500 + 1000;
            LevelNumber.texture = Number2;
            LevelFrame.sprite = LevelType1;
            LevelSliderImage.sprite = SliderType1;
            //2
        }
        else if (Data.Player.Reputation >= 1500 && Data.Player.Reputation < 4000)
        {
            LevelSlider.minValue = 1500;
            LevelSlider.maxValue = 1500 + 2500;
            LevelNumber.texture = Number3;
            LevelFrame.sprite = LevelType2;
            LevelSliderImage.sprite = SliderType2;
            //3
        }
        else if (Data.Player.Reputation >= 4000 && Data.Player.Reputation < 8000)
        {
            LevelSlider.minValue = 4000;
            LevelSlider.maxValue = 4000 + 4000;
            LevelNumber.texture = Number4;
            LevelFrame.sprite = LevelType2;
            LevelSliderImage.sprite = SliderType2;
            //4
        }
        else if (Data.Player.Reputation >= 8000 && Data.Player.Reputation < 14000)
        {
            LevelSlider.minValue = 8000;
            LevelSlider.maxValue = 8000 + 6000;
            LevelNumber.texture = Number5;
            LevelFrame.sprite = LevelType2;
            LevelSliderImage.sprite = SliderType2;
            //5
        }
        else if (Data.Player.Reputation >= 14000 && Data.Player.Reputation < 22000)
        {
            LevelSlider.minValue = 14000;
            LevelSlider.maxValue = 14000 + 8000;
            LevelNumber.texture = Number6;
            LevelFrame.sprite = LevelType3;
            LevelSliderImage.sprite = SliderType3;
            //6
        }
        else if (Data.Player.Reputation >= 22000 && Data.Player.Reputation < 32000)
        {
            LevelSlider.minValue = 22000;
            LevelSlider.maxValue = 22000 + 10000;
            LevelNumber.texture = Number7;
            LevelFrame.sprite = LevelType3;
            LevelSliderImage.sprite = SliderType3;
            //7
        }
        else if (Data.Player.Reputation >= 32000 && Data.Player.Reputation < 47000)
        {
            LevelSlider.minValue = 32000;
            LevelSlider.maxValue = 32000 + 15000;
            LevelNumber.texture = Number8;
            LevelFrame.sprite = LevelType3;
            LevelSliderImage.sprite = SliderType3;
            //8
        }
        else if (Data.Player.Reputation >= 47000 && Data.Player.Reputation < 77777)
        {
            LevelSlider.minValue = 47000;
            LevelSlider.maxValue = 47000 + 30777;
            LevelNumber.texture = Number9;
            LevelFrame.sprite = LevelType4;
            LevelSliderImage.sprite = SliderType4;
            //9
        }
        else if(Data.Player.Reputation < 500)
        {
            LevelSlider.minValue = 0;
            LevelSlider.maxValue = 500;
            LevelNumber.texture = Number1;
            LevelFrame.sprite = LevelType1;
            LevelSliderImage.sprite = SliderType1;
            //default
        } 
        #endregion

        LevelSlider.value = Data.Player.Reputation;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
