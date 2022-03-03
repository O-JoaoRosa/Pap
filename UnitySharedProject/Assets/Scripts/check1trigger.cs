using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class check1trigger : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //associa o evento criado no script eventController com o metodo
        //EventController.current.onCheck1 += ShowCheck1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        EventController.current.Check1Exit();
    }

    public void ShowCheck1()
    {
        Time.timeScale = 0.1f;
        tutorial.isTextShowing = true;
    }
}
