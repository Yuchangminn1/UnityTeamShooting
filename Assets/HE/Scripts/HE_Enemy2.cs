using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HE_Enemy2 : MonoBehaviour
{
    public float Speed = 3f;
    public float Delay = 1f;
    public float sec = 2;
    public float HP = 100;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform pos;
    [SerializeField] GameObject item;
    [SerializeField] int monsterScore;

    Vector2 vec2;

    void Start()
    {
        InvokeRepeating("CreateBullet", Delay, sec);
    }

    void CreateBullet()
    {
        
        GameObject go = Instantiate(bullet,pos.position,Quaternion.identity);
        HE_EnemyBullet HE_bullet = go.GetComponent<HE_EnemyBullet>();
        if(HE_bullet != null)
        {
            HE_bullet.SetDir(Vector3.down);
        }
    }

    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }
    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    public void Damage(int Attack)
    {
        HP -= Attack;
        if(HP < 0)
        {
            HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();
            //Player playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerLogic.score += monsterScore; //몬스터가 파괴 될 때 플레이어에게 점수 추가해줌

            Destroy(gameObject);
            ItemDrop();
        }
    }
    public void ItemDrop()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HE_Wall"))
        {
            Destroy(gameObject);   
        }
    }


}
