using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
       if(player != null)
        {
            Debug.Log("플레이어 감지됨");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyLazer"))
        {
            Destroy(player);
        }
        //if(collision.gameObject.CompareTag("Bullet"))
        //{
        //    Destroy(GameObject.Find("Player"));
        //}
    }
}
