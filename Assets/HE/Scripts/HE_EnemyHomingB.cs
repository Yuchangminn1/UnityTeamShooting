using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE_EnemyHomingB : MonoBehaviour
{
    GameObject target;
    public float speed = 3.0f;
    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        if (target == null)
            return;

        dir = target.transform.position - transform.position;
        dirNo = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dirNo * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //GameObject.Find("HE_GameScene").gameObject.GetComponent<HE_GameManager>().UpdateLifeIcon();  

            //Destroy(collision.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
