using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlinkEffect_WhenHit : MonoBehaviour
{
    SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    IEnumerator Hit_Blink_Effect(SpriteRenderer spr, Color OriginalColor, Color NewColor)
    {
        spr.color = NewColor;
        yield return new WaitForSeconds(0.03f);
        spr.color = OriginalColor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
            StartCoroutine(Hit_Blink_Effect(sp,Color.white,Color.black));
    }

}
