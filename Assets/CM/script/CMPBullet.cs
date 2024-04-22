using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMPBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "CMBoss")
        {
            collision.GetComponent<BossDieCheck>().Damage(gameObject.GetComponent<PBullet>().dmg,transform.position);
            Destroy(gameObject);
        }

    }
}
