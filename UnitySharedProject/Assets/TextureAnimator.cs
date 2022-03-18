using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimator : MonoBehaviour
{
    public float XSpeed;
    public float YSpeed;

    // Update is called once per frame
    void Update()
    {
        float offsetX = Time.time * XSpeed;
        float offsetY = Time.time * YSpeed;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
