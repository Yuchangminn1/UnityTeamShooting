using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator ani;
    public float speed;
    public Transform pos = null;

    public GameObject [] Pbullet = null; 

     //Rigidbody2D rb;

    public int life; //���
    public int score;
    public GameManager manager;

    //�̻��� ��
    public int power = 0;


    //������
    public bool isDmg;


    void Start()
    {
        ani = GetComponent<Animator>();
        InvokeRepeating("Shoot", 0, 0.2f);
    }


    void Update()
    {
        Move();
    }

    void Move() //�̵�
    {
        float moveX = Input.GetAxisRaw("Horizontal"); //���� ��
        float moveY = Input.GetAxisRaw("Vertical"); // ���� ��

        Vector3 curPos = transform.position; //���� ��ġ
        Vector3 nextPos = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime; // �̵��� ��ġ 

        transform.position = curPos + nextPos; //�̵��� ���� ��ġ = ���� ���� ��ġ + �̵� �� ��ġ

        //�÷��̾� ȭ�� ������ ������ �ʰ� �ϱ�
        if (transform.position.x >= 5.0f)
            transform.position = new Vector3(5.0f, transform.position.y, 0);
        if (transform.position.x <= -5.0f)
            transform.position = new Vector3(-5.0f, transform.position.y, 0);
        if (transform.position.y >= 9.3f)
            transform.position = new Vector3(transform.position.x, 9.3f, 0);
        if (transform.position.y <= -9.3f)
            transform.position = new Vector3(transform.position.x, -9.3f, 0);
    }
    void Shoot() //�̻��� 
    {
        SoundManager.instance.PlaySound("PBullet1");
        Instantiate(Pbullet[power], pos.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)  
    {
    //    if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "MBullet")
    //    {
    //        //if (isRespawnTime) // ���� �ð��̸� ������ ���� ����
    //        //    return;

    //        if (isDmg) //��� �ΰ� ������� �� ����
    //            return;
    //        isDmg = true;

    //        life--; //��� ����
    //        manager.UpdateLifeIcon(life);

    //        if (life == 0)
    //        {
    //            manager.GameOver();
    //        }
    //        else
    //        {
    //            manager.RespawnPlayer();
    //        }
    //        //�÷��̾� ��Ȱ��ȭ
    //        gameObject.SetActive(false);
    //        Destroy(collision.gameObject);
    //        //�÷��̾ ���� ���� �Ѿ� ��Ȱ��ȭ
    //        //collision.gameObject.SetActive(false); 

    //    }

        if (collision.tag == "Item")
        {
            power += 1;
            if (power >= 3)
                power = 2;
            //�浹�� ������ �����
            Destroy(collision.gameObject);
        }
    }
}
