using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE_Enemy1 : MonoBehaviour
{
    public float Speed = 3f;
    public float Delay = 0.2f;
    public float sec = 1;
    public float HP = 100;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform pos;
    [SerializeField] GameObject item;
    [SerializeField] int monsterScore;
    Vector2 vec2 = Vector2.down;

    void Start()
    {
        InvokeRepeating("CreateBullet", Delay, sec);
    }
    void Update()
    {
        transform.Translate(vec2 * Speed * Time.deltaTime);
    }
    void CreateBullet()
    {
        GameObject go = Instantiate(bullet, pos.position, Quaternion.identity);
        HE_EnemyBullet HE_Bullet = go.GetComponent<HE_EnemyBullet>();
        if(HE_Bullet != null)
        {
            HE_Bullet.SetDir(new Vector3(0, -1, 0));
        }
    }
    public void Damage(int Attack)
    {
        HP -= Attack;
        if (HP < 0)
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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
