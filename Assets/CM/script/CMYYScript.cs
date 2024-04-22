using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMYYScript : MonoBehaviour
{
    float MyA = 1f;
    private void Start()
    {
        StartCoroutine(YY());
    }
    IEnumerator YY()
    {
        while (MyA > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, MyA);
            MyA -= 0.05f;
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
