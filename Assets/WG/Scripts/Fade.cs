using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    float time = 0;
    void Start()
    {
    }

    private void Update()
    {
        time += Time.deltaTime;
        GetComponent<Image>().color = new Color(0, 0, 0, time*1f);
    }
}
