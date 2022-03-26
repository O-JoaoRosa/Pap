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
    [SerializeField] private Rigidbody carMotor;
    private float distanceTravelled = 0;
    private float distanceTravelled2 = 0;
    private float distanceTravelled3 = 0;
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

    private void FixedUpdate()
    {
        
        //verifica se a tecla shift foi ou está a ser carregada
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Nitro atctivo");

            Debug.Log("Nitro 1 : " + nitroImageOrange.fillAmount+ " nitro 2 : " + nitroImageBlue.fillAmount + " nitro 3 : " + nitroImagePurple.fillAmount);
            //verficia em que nivel é que o nitro se encontra e muda a força do nitro assim como o seu consumo
            if (nitroImageOrange.fillAmount >= 0f && nitroImageBlue.fillAmount <= 0.0001f && nitroImagePurple.fillAmount <= 0.0001f)
            {
                //adiciona força ao motor do carro
                carMotor.AddForce(transform.forward * 10f, ForceMode.Acceleration);

                if (cameraScript.defaultFOV < 70)
                {
                    cameraScript.defaultFOV += 0.1f;
                }

                //gasta o nitro
                nitroImageOrange.fillAmount -= 0.005f;
            }
            else if (nitroImageOrange.fillAmount >= 1f && nitroImageBlue.fillAmount > 0f && nitroImagePurple.fillAmount < 0.0001f)
            {
                //adiciona força ao motor do carro
                carMotor.AddForce(transform.forward * 20f, ForceMode.Acceleration);

                if (cameraScript.defaultFOV < 75)
                {
                    cameraScript.defaultFOV += 0.2f;
                }

                //gasta o nitro
                nitroImageBlue.fillAmount -= 0.009f;
            }
            else if (nitroImageOrange.fillAmount >= 1f && nitroImageBlue.fillAmount >= 1f && nitroImagePurple.fillAmount > 0.0001f)
            {
                //adiciona força ao motor do carro
                carMotor.AddForce(transform.forward * 30f, ForceMode.Acceleration);

                if (cameraScript.defaultFOV < 85)
                {
                    cameraScript.defaultFOV += 0.5f;
                }

                //gasta o nitro
                nitroImagePurple.fillAmount -= 0.012f;

            }


            distanceTravelled = nitroImageOrange.fillAmount * maxNitro;
            distanceTravelled2 = nitroImageBlue.fillAmount * (maxNitro * 1.25f);
            distanceTravelled3 = nitroImagePurple.fillAmount *(maxNitro * 1.5f);
        }
        else if (cameraScript.defaultFOV > 60)
        {
            cameraScript.defaultFOV -= 0.1f;
        }
    }

    /// <summary>
    /// metodo usado para descobrir a distancia que o carro fez drift
    /// </summary>
    public void NitroPoints()
    {
        //verifica qual é o nivel a que o nitro se encontra e adiciona pontos dependedo da distancia 
        if (nitroImageOrange.fillAmount >= 1f && nitroImageBlue.fillAmount >= 0f && nitroImagePurple.fillAmount <= 0f && distanceTravelled >= maxNitro && distanceTravelled2 < (maxNitro * 1.25f))
        {
            distanceTravelled2 += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            nitroImageBlue.fillAmount = distanceTravelled2 / (maxNitro * 1.25f);
        }
        else if (nitroImageOrange.fillAmount >= 1f && nitroImageBlue.fillAmount >= 1f && nitroImagePurple.fillAmount >= 0f && distanceTravelled2 >= (maxNitro * 1.25f))
        {
            distanceTravelled3 += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            nitroImagePurple.fillAmount = distanceTravelled3 / (maxNitro * 1.5f);
        }
        else if(nitroImageOrange.fillAmount >= 0f && nitroImageBlue.fillAmount <= 0f && nitroImagePurple.fillAmount <= 0f)
        {
            distanceTravelled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            nitroImageOrange.fillAmount = distanceTravelled / maxNitro;
        }
    }
}
