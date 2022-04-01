using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Thread.Sleep(800);
        gameObject.LeanMoveLocal(new Vector3(0f, 2.8f, 2.2f),1f).setEaseOutBounce().setOnComplete(()=> { CarControllerLobby.canMove = true; CarControl.canMove = true; });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
