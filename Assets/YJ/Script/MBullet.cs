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
        //�÷��̾� ã��
        target = GameObject.FindGameObjectWithTag("Player");

        //�÷��̾� - �̻���
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
            //�Ѿ˸� ���� -> �÷��̾�� �÷��̾� ��ũ��Ʈ���� �����Ǳ� ������

            //GameObject.Find("GameManager").gameObject.GetComponent<GameManager>().UpdateLifeIcon();  
        }
    }
}
