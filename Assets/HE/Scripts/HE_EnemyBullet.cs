using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE_EnemyBullet : MonoBehaviour
{
    public float Speed = 3f;
    public Vector3 myDir;
    void Start()
    {
        
    }

    public void SetDir(Vector3 dir)
    {
        myDir = dir;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(myDir * Speed * Time.deltaTime);
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
