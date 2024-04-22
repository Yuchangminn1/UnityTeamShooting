using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDieCheck : MonoBehaviour
{
    public int HP = 1000;
    public GameObject Explosionx;
    public GameObject PBulletEx;
    public bool Dead = false;

    public void Damage(int attack,Vector3 ExPos)
    {
        HP -= attack;
        GetComponent<SpriteRenderer>().color = Color.black;
        //Instantiate(PBulletEx,ExPos,Quaternion.identity);    

        if (HP < 0)
        {
            Dead = true;
            GetComponent<Collider2D>().enabled = false;
            Instantiate(Explosionx, transform.position, Quaternion.identity);
            WG_SoundManager.instance.audioSource_Shot.volume = 10f;
            WG_SoundManager.instance.ShootingSound(1);
            GetComponent<CMBossAttack>().StopCoroutine($"S{GetComponent<CMBossAttack>().CAttackType}");  






        }
    }
    
    private void FixedUpdate()
    {
        if(Dead == false) { ReturnColor(); }
    }
    void ReturnColor()
    {
        if (GetComponent<SpriteRenderer>().color != Color.white)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(collision.gameObject);
    //}


}
