using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float Meteor_BIG_HP, Meteor_Wide_HP;
    public float Force = 17f;
    public GameObject[] Debris;
    public GameObject Effect;
    public GameObject[] Items;
    public static string MeteorType;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var enemydata = GameObject.Find("GameManager").GetComponent<EnemyData>();
        Meteor_BIG_HP = enemydata.Meteor_BIG_HP_p;
        Meteor_Wide_HP = enemydata.Meteor_Wide_HP_p;
    }
    private void Update()
    {
        if (Meteor_BIG_HP <= 0 || Meteor_Wide_HP <= 0) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (rb.velocity.y <= 7f)
            {
                rb.AddForce(Vector2.up * Force, ForceMode2D.Impulse);
                if (rb.velocity.y >= 7f)
                {
                    if (collision.gameObject.CompareTag("Enemy"))
                    {
                        //적이 강하게 튕겨낸 운석에 맞으면 체력 크게 감소
                        Destroy(gameObject);
                    }
                }
            }
            switch (MeteorType)
            {
                case "Big": Meteor_BIG_HP -= 5; break;
                case "Wide": Meteor_Wide_HP -=5; break;
            }


        }
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player_HitBox"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }
    private void OnDestroy()
    {
        int ib = Random.Range(3, 9);
        for (int a = 0; a < ib; a++)
        {
            for (int i = 0; i < Debris.Length; i++)
            {
                Vector2 V = new Vector2(transform.position.x + Random.Range(0.1f, 2f), transform.position.y + Random.Range(0.1f, 2f));
                Instantiate(Debris[i], V, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));

            }
        }

        if (Random.value <= 1f) Instantiate(Items[0], transform.position, Quaternion.identity);
    }

}