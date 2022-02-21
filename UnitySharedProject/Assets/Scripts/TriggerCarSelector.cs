using System.Threading;
using UnityEngine;

public class TriggerCarSelector : MonoBehaviour
{
    static bool canTrigger = true;

    [SerializeField]
    [Header("camera")]
    private Camera mainCamera;


    [SerializeField]
    [Header("player")]
    private GameObject PlayerCar;
    public GameObject Sphere;

    public static Vector3 cameraOriginalPos;
    private GameObject ModelCar;


    private void Start()
    {
        //associa o evento criado no script eventController com o metodo
        EventController.current.onCarSelectorEnter += CamaraSet;
        EventController.current.onCarSelectorExit += ResetCam;
        cameraOriginalPos = mainCamera.transform.position;
    }

    private void Update()
    {
        //verifica se o botão esc foi carregado para sair da pausa
        if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Escape) && !canTrigger)
        {
            mainCamera.transform.LeanMoveLocal(cameraOriginalPos, 2f).setEaseInOutExpo();
            EventController.current.CarSelectorExit();
            canTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if he can trigger the menu again or not
        if (canTrigger)
        {
            Sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Sphere.transform.position = new Vector3(38.5f, 0f, -13.6999998f);
            EventController.current.CarSelectorEnter();
            canTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canTrigger = true;

    }

    private void ResetCam()
    {
        lookAt.StartLooking();
        CarManager.UpdateCarUsed();
        if (GameObject.Find("Car(Clone)"))
        {
            PlayerCar.transform.position = GameObject.Find("Car(Clone)").transform.position;
        }
        else
        {
            PlayerCar.transform.position = GameObject.Find("Car").transform.position;
        }
       
        mainCamera.transform.LeanMoveLocal(cameraOriginalPos, 1.5f).setEaseOutExpo();
        PlayerCar.SetActive(true);

    }


    private void CamaraSet()
    {
        lookAt.StopLooking();
        Thread.Sleep(10);
        Debug.Log("a encontrar carro");
        ModelCar = GameObject.Find("CarRoot/CarModel/Car");
        Destroy(GameObject.Find("CarRoot/CarModel/Car"));
        PlayerCar.SetActive(false);
        Debug.Log("a iniciar animações");
        mainCamera.transform.LeanMoveLocal(new Vector3(6.32f, 12.13f, -31.54f), 2f).setEaseOutExpo().callOnCompletes();

        Vector3 pos = gameObject.transform.position;
        pos.y = pos.y + 4.82f;
        Instantiate(ModelCar, pos, new Quaternion(0,0f,0,0)).LeanRotate(new Vector3(0,-144f,0), 0f);
    }
}
