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
        //�ʱ�ȭ
        sprite = GetComponent<SpriteRenderer>();
        // rig = GetComponent<Rigidbody2D>();

        //������ �ӷ�
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

        Invoke("CreateBullet", Delay); //1�ʸ��� ȣ��

    }

    void CreateBullet()
    {
        Instantiate(Mbullet, ms.position, Quaternion.identity);
        Invoke("CreateBullet", Delay); // �ݺ� ȣ��
    }

    public void Damage(int dmg)
    {
        HP -= dmg;

        if(PlayerControlManager.Instance.GetPlayer() != null && PlayerControlManager.Instance.GetPlayer().activeSelf == true) //null�� �ƴϸ�
        {
            //playerLogic ������  
            HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();      
            playerLogic.score += monsterScore; //���Ͱ� �ı� �� �� �÷��̾�� ���� �߰�����
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
            //�÷��̾� �̻��Ͽ� ������ ������ ����
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
