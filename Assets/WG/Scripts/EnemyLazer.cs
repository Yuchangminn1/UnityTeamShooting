using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : Poolable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") || collision.gameObject.CompareTag("Meteor"))
        {
            Destroy(collision.gameObject);
        }
      //  if (collision.gameObject.CompareTag("EnemyBullet"))
    }
}
