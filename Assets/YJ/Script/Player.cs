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

    public int life; //목숨
    public int score;
    public GameManager manager;

    //미사일 힘
    public int power = 0;


    //데미지
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

    void Move() //이동
    {
        float moveX = Input.GetAxisRaw("Horizontal"); //수평 값
        float moveY = Input.GetAxisRaw("Vertical"); // 수직 값

        Vector3 curPos = transform.position; //현재 위치
        Vector3 nextPos = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime; // 이동할 위치 

        transform.position = curPos + nextPos; //이동한 현재 위치 = 원래 현재 위치 + 이동 할 위치

        //플레이어 화면 밖으로 나가지 않게 하기
        if (transform.position.x >= 5.0f)
            transform.position = new Vector3(5.0f, transform.position.y, 0);
        if (transform.position.x <= -5.0f)
            transform.position = new Vector3(-5.0f, transform.position.y, 0);
        if (transform.position.y >= 9.3f)
            transform.position = new Vector3(transform.position.x, 9.3f, 0);
        if (transform.position.y <= -9.3f)
            transform.position = new Vector3(transform.position.x, -9.3f, 0);
    }
    void Shoot() //미사일 
    {
        SoundManager.instance.PlaySound("PBullet1");
        Instantiate(Pbullet[power], pos.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)  
    {
    //    if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "MBullet")
    //    {
    //        //if (isRespawnTime) // 무적 시간이면 적에게 맞지 않음
    //        //    return;

    //        if (isDmg) //목숨 두개 사라지는 것 방지
    //            return;
    //        isDmg = true;

    //        life--; //목숨 제거
    //        manager.UpdateLifeIcon(life);

    //        if (life == 0)
    //        {
    //            manager.GameOver();
    //        }
    //        else
    //        {
    //            manager.RespawnPlayer();
    //        }
    //        //플레이어 비활성화
    //        gameObject.SetActive(false);
    //        Destroy(collision.gameObject);
    //        //플레이어가 맞은 적과 총알 비활성화
    //        //collision.gameObject.SetActive(false); 

    //    }

        if (collision.tag == "Item")
        {
            power += 1;
            if (power >= 3)
                power = 2;
            //충돌된 아이템 지우기
            Destroy(collision.gameObject);
        }
    }
}
