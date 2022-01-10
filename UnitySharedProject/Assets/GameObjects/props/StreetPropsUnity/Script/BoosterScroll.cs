using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterScroll : MonoBehaviour
{
    [Range(-1.5f, 0.0f)]
    public float scrollSpeed = -0.5f;

    private Material scrollMaterial;
    private float textureOffset;

    private void Start()
    {
        scrollMaterial = GetComponent<Renderer>().materials[1];
    }

    void Update()
    {
        textureOffset += scrollSpeed * Time.deltaTime;
        scrollMaterial.SetTextureOffset("_MainTex", new Vector2(0.0f, textureOffset + scrollSpeed * Time.deltaTime));
        scrollMaterial.SetTextureOffset("_EmissionMap", new Vector2(0.0f, textureOffset + scrollSpeed * Time.deltaTime));
    }
}
