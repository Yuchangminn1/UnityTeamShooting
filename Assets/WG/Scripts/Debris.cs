using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Player_HitBox"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }
}
