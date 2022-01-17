using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    [Header("Speeds")]
    //speeds

    static public int rarity = 1;
    [SerializeField] static public float fowardSpeed = 80;
    [SerializeField] static public float reverseSpeed = 35;
    [SerializeField] static public float turnSpeed = 40;
    [SerializeField] static public float groundDrag = 0.8f;
    [SerializeField] static public float airDrag = 0.5f;
}
