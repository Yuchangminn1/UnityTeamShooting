using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 13f;
    public int HP;
    public float Delay = 1;
    public GameObject Mbullet = null;
    public Transform ms = null;
    public Sprite[] sprites;
    public GameObject player;
    public int monsterScore;

    public GameObject Item = null;

   // Rigidbody2D rig;
    SpriteRenderer sprite;
    private void Awake()
    {
        //초기화
        sprite = GetComponent<SpriteRenderer>();
        // rig = GetComponent<Rigidbody2D>();

        //몬스터의 속력
        // rig.velocity = Vector2.down * speed * Time.deltaTime;

    }
    IEnumerator DestroyMonsterForSeconds()
    {
        yield return new WaitForSeconds(6);  
        Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(DestroyMonsterForSeconds());

        transform.Translate(Vector2.down * speed * Time.deltaTime);
        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("CreateBullet", Delay); //1초마다 호출

    }

    void CreateBullet()
    {
        Instantiate(Mbullet, ms.position, Quaternion.identity);
        Invoke("CreateBullet", Delay); // 반복 호출
    }

    public void Damage(int dmg)
    {
        HP -= dmg;

        if(PlayerControlManager.Instance.GetPlayer() != null && PlayerControlManager.Instance.GetPlayer().activeSelf == true) //null이 아니면
        {
            //playerLogic 가져옴  
            HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();      
            playerLogic.score += monsterScore; //몬스터가 파괴 될 때 플레이어에게 점수 추가해줌
            if (HP <= 0)
            {
                ItemDrop();
                Destroy(gameObject);
            }
        }

    }

    void Update()
    {
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void ItemDrop()
    {
        Instantiate(Item, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PBullet") 
        {
            //플레이어 미사일에 맞으면 데미지 입음
            PBullet pBullet = collision.gameObject.GetComponent<PBullet>();
            Damage(pBullet.dmg);
        }

        if (collision.gameObject.CompareTag("HE_Wall"))
        {
            Debug.Log("HE_Wall");     
            Destroy(gameObject);
        }
    }
}
