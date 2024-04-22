using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE_BackGround : MonoBehaviour
{
    public float ScorllSpeed = 0.3f;
    Material myMaterial;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float newOffsetY = myMaterial.mainTextureOffset.y + ScorllSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(0, newOffsetY);
        myMaterial.mainTextureOffset = newOffset;

    }
}
