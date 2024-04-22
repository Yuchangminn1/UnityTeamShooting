using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scroll = 0.01f;
    Material material; 

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }


    void Update()
    {
        float newOffsetY = material.mainTextureOffset.y + scroll* Time.deltaTime;
        Vector2 newOffset = new Vector2(0, newOffsetY);

        material.mainTextureOffset = newOffset;
    }
}
