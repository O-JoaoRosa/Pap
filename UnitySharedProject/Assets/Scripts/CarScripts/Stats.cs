using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //TODO : preciso de comentar isto para saber oq cada faz
    //TODO : nao esquecer de enquanto tiver no meu pc � melhor fazer a api para os carros 

    [SerializeField] static public float rarity {get; set;}
    [SerializeField] static public float fowardSpeed {get; set;}
    [SerializeField] static public float reverseSpeed { get; set; }
    [SerializeField] static public float turnSpeed { get; set; }
    [SerializeField] static public float groundDrag { get; set; }
    [SerializeField] static public float airDrag { get; set; }
    [SerializeField] static public string carName { get; set; }
    static public float defaultTurnAngle { get; set; }
    static public float DriftTurnAngle { get; set; }

    private void Awake()
    {
        fowardSpeed = 25f;
        reverseSpeed = fowardSpeed * 0.45f;
        defaultTurnAngle = 30f;
        DriftTurnAngle = 50f;
        turnSpeed = 45f;
        groundDrag = 0.75f;
        airDrag = 0.4f;
        carName = "carName";
    }
}
