using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBullet : MonoBehaviour
{
    public GameObject target ;
    public float speed = 5;
    Vector2 dir;
    Vector2 dirNo;
    void Start()
    {
        //플레이어 찾기
        target = GameObject.FindGameObjectWithTag("Player");

        //플레이어 - 미사일
        if(target == null)
            return;


        dir = target.transform.position - transform.position;
        dirNo = dir.normalized;
    }


    void Update()
    {
        transform.Translate(dirNo * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //총알만 삭제 -> 플레이어는 플레이어 스크립트에서 삭제되기 떄문에

            //GameObject.Find("GameManager").gameObject.GetComponent<GameManager>().UpdateLifeIcon();  
        }
    }
}
