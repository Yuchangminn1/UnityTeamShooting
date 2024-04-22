using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMPColor : MonoBehaviour
{
    [SerializeField]
    float lerpTime = 0.1f; //��� ǥ��
    [SerializeField]  Text textWarning;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
        WG_SoundManager.instance.audioSource_Shot.volume = 1f;
        WG_SoundManager.instance.ShootingSound(5);
        WG_SoundManager.instance.audioSource_Shot.volume = 0.2f;
    }

    IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));

        }
    }

    IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        //���� �ε巴�� �ٲٵ���
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            textWarning.color = Color.Lerp(startColor,endColor,percent);
            yield return new WaitForEndOfFrame();

        }
        //�� ������ ��ٸ�
        yield return null;
    }
  
}
