using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nitro : MonoBehaviour
{

    [SerializeField] private float maxNitro;
    [SerializeField] private Image nitroImageOrange;
    [SerializeField] private Image nitroImageBlue;
    [SerializeField] private Image nitroImagePurple;
    public float distanceTravelled = 0;
    public float distanceTravelled2 = 0;
    public float distanceTravelled3 = 0;
    private Vector3 lastPosition;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastPosition = transform.position;
        }
    }

    /// <summary>
    /// metodo usado para descobrir a distancia que o carro fez drift
    /// </summary>
    public void NitroPoints()
    {
        if (distanceTravelled > maxNitro && distanceTravelled2 < maxNitro)
        {
            distanceTravelled2 += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            nitroImageBlue.fillAmount = distanceTravelled2 / maxNitro ;
        }
        else if (distanceTravelled2 > maxNitro)
        {
            distanceTravelled3 += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            nitroImagePurple.fillAmount = distanceTravelled3 / maxNitro;
        }
        else
        {
            distanceTravelled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            nitroImageOrange.fillAmount = distanceTravelled / maxNitro;
        }
    }
}
