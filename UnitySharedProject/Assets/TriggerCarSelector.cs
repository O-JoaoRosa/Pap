using UnityEngine;

public class TriggerCarSelector : MonoBehaviour
{
    bool canTrigger = true;

    [SerializeField]
    [Header("camera")]
    private Camera mainCamera;


    [SerializeField]
    [Header("player")]
    private GameObject PlayerCar;

    private Transform cameraOriginalPos;
    private GameObject ModelCar;


    private void Start()
    {
        //associa o evento criado no script eventController com o metodo
        EventController.current.onCarSelectorEnter += CamaraSet;
        cameraOriginalPos = mainCamera.transform;
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
        //miranha
        EventController.current.CarSelectorExit();
        canTrigger = true;

    }

    private void CamaraSet()
    {
        ModelCar = PlayerCar.transform.Find("ModelCar/Car").gameObject;
        if (ModelCar != null)
        {
            Debug.Log("child found");
        }
        else
        {
            Debug.Log("fuxck");
        }

        mainCamera.transform.LeanMoveLocal(new Vector3(6.32f, 12.13f, -31.54f), 2f).setEaseInBack();
        
        Destroy(PlayerCar, 0.1f);
        Instantiate(ModelCar, gameObject.transform.position, new Quaternion(0,0,0,0)); 
    }
}
