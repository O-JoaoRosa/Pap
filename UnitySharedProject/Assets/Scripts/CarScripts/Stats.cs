using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] static public float rarity {get; set;}
    [SerializeField] static public float fowardSpeed {get; set;}
    [SerializeField] static public float reverseSpeed { get; set; }
    [SerializeField] static public float turnSpeed { get; set; }
    [SerializeField] static public float groundDrag { get; set; }
    [SerializeField] static public float airDrag { get; set; }
    [SerializeField] static public string carName { get; set; }

    private void Awake()
    {
        fowardSpeed = 20f;
        reverseSpeed = fowardSpeed * 0.45f;
        turnSpeed = 45f;
        groundDrag = 0.75f;
        airDrag = 0.4f;
        carName = "carName";
    }
}
