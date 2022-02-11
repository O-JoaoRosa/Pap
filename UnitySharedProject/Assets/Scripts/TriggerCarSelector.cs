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
            mainCamera.transform.LeanMoveLocal(cameraOriginalPos.position, 2f).setEaseInOutExpo();
            EventController.current.CarSelectorExit();
            canTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if he can trigger the menu again or not
        if (canTrigger)
        {
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
        PlayerCar.transform.position = GameObject.Find("Car(Clone)").transform.position;
        mainCamera.transform.LeanMoveLocal(cameraOriginalPos, 2f).setEaseOutExpo();
        PlayerCar.SetActive(true);

    }


    private void CamaraSet()
    {
        ModelCar = PlayerCar.transform.Find("CarModel/Car").gameObject;

        PlayerCar.SetActive(false);

        mainCamera.transform.LeanMoveLocal(new Vector3(6.32f, 12.13f, -31.54f), 2f).setEaseOutExpo();
        mainCamera.transform.LeanRotate(new Vector3(5f,88f,0.728f),1f);

        Vector3 pos = gameObject.transform.position;
        pos.y = pos.y + 4.82f;
        ModelCar.transform.localScale *= 3.3f;
        Instantiate(ModelCar, pos, new Quaternion(0,0f,0,0)).LeanRotate(new Vector3(0,-144f,0), 0f);
    }
}
