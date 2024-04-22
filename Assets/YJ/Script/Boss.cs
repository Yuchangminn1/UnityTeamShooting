using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Boss : MonoBehaviour
{
    public GameObject Bossbullet; //보스 미사일
    public GameObject Bossbullet2;
    public GameObject Bossbullet3;
    public GameObject Bossbullet4;
    [SerializeField] GameObject DestroyEffect;

    public Transform tr; // 미사일 위치
    public Transform tr2;
    public Transform tr3;

    int flag = 1;
    int speed = 9;

    public int HP = 100;
    public int MaxHp = 1000;
    public GameObject player;
    public GameObject Bullet;
    public int monsterScore;


    public int PatternIndex; //패턴
    public int curPatternCount; //현재 패턴 
    public int[] maxPatternCount;



    public bool MoveDown = false;

    bool bossAttackPattern1Done;
    bool bossAttackPattern2Done;
    bool bossAttackPattern3Done;

    bool isSceneMoved = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.position = new Vector3(0,10.0f, 0); //화면 밖에 있음  
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        StartCoroutine("Move");

        StartCoroutine(BossSpawn());

    }
    void Update()
    {
        //좌우로 움직임
        if (transform.position.x >= 2.0f)
        {
            flag *= -1;
        }
        if (transform.position.x <= -2.0f)
        {
            flag *= -1;
        }
        transform.Translate(flag * speed * Time.deltaTime, 0, 0);

    }

    IEnumerator Move()
    {
        while (true)
        {
            if (MoveDown == false)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);

                if (transform.position.y <= 6.3f)
                {
                    MoveDown = true;
                    yield break;
                }
            }
            yield return null;
        }
    }


    IEnumerator BossSpawn()
    {

        WaitForSeconds seconds = new WaitForSeconds(2f);
        while (true)
        {
            yield return seconds; //패턴 전환

            Pattern();
        }
    }



    void Pattern()
    {
        //패턴이 3개면 배열 0-2
        //curPatternCount = 0;
        if (PatternIndex > 2)
            PatternIndex = 0;

        switch (PatternIndex)
        {
            case 0:
                StartCoroutine(BossBullet());
                break;
            case 1:
                StartCoroutine(BulletSecond());    
               break;
            case 2:
                StartCoroutine(CircleFire());
                break;
        }
        PatternIndex += 1;
    }

    IEnumerator BossBullet()
    {
        Instantiate(Bossbullet, tr.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);

        Instantiate(Bossbullet2, tr2.position, Quaternion.identity);

        Instantiate(Bossbullet3, tr3.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);

        //while (true)
        //{
        //    //미사일 3개 발사
        //    Instantiate(Bossbullet, tr.position, Quaternion.identity);
        //    Instantiate(Bossbullet2, tr2.position, Quaternion.identity);
        //    Instantiate(Bossbullet3, tr3.position, Quaternion.identity);

        //    yield return new WaitForSeconds(2f);

        //}
    }


    IEnumerator BulletSecond()
    {
        Vector3 target = Vector3.zero; //목표 위치(중앙)


        while (true)
        {
            if(transform.position.x <= 5.0f ||
               transform.position.x >= 5.0f)
            {

                if (HP <= MaxHp * 0.5f) //체력
                {
                    StartCoroutine(BossBullet());

                    yield return new WaitForSeconds(0.3f);
                }   

            }
            yield return null;
        }

    }


    IEnumerator CircleFire()
    {
        float attackRate = 2; //공격 주기
        int count = 10; // 발사체 갯수
        float intervalAngle = 360 / count;
        float weightAngle = 0;


        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                //발사체 보스 위치에서 생성
                GameObject clone = Instantiate(Bossbullet4, transform.position, Quaternion.identity);

                //발사체 이동 방향(각도) 
                float angle = weightAngle + intervalAngle * i;

                //발사체 이동 방향 (벡터)각도를 원형으로 바꿈
                //Cos(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                // float x = Mathf.Cos(angle * Mathf.Deg2Rad);으로 써도 똑같음

                //sin(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                clone.GetComponent<BossBullet>().Move(new Vector2(x, y)); //그 방향으로 날아감
            }

            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1; //0도에서 나가면 안됨

            //attackRate 시간만큼 대기 - 계속 쏘면 너무 빨리 나가게 됨
            yield return new WaitForSeconds(attackRate); //2초마다 원형 미사일 발사
        }
    }





    public void Damage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Destroy(gameObject);  // 보스 죽고 
            /* 보스 사망시 이펙트 처리 */
            WG_SoundManager.instance.audioSource_Shot.volume = 10f;

            GameObject go = Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(go, 1);
            WG_SoundManager.instance.ShootingSound(1);
            WG_SoundManager.instance.audioSource_Shot.volume = 0.2f;
            Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
            if (monsters.Length > 0)
            {
                foreach (var m in monsters)
                {
                    Destroy(m.gameObject);
                }
            }

            MBullet[] bullets = GameObject.FindObjectsOfType<MBullet>();
            if (bullets.Length > 0)
            {
                foreach (var b in bullets)
                {
                    Debug.Log("bbbbbb");  
                    Destroy(b.gameObject);    
                }
            }

            HE_Item[] Items = GameObject.FindObjectsOfType<HE_Item>();
            if (Items.Length > 0)
            {
                foreach (var i in Items)
                {
                    Debug.Log("bbbbbb");
                    Destroy(i.gameObject);
                }
            }  

            if (PlayerControlManager.Instance.GetPlayer() != null) //null이 아니면
            {

                HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();
                playerLogic.score += monsterScore; //몬스터가 파괴 될 때 플레이어에게 점수 추가해줌  

                CanvasManager.Instance.OpenClearPanel();
                CanvasManager.Instance.OpenScorePanel();

                PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = true; // 총알이 멈출 꺼에요.   
                HE_Player.JustOne = false;
                //Transform startPos = PlayerControlManager.Instance.GetStartPos().transform;  
                //PlayerControlManager.Instance.GetPlayer().transform.DOMove(startPos.position, 2).SetEase(Ease.Linear).OnComplete(PlayerUpMove);  

                Transform endPos = PlayerControlManager.Instance.GetEndPos().transform;
                PlayerControlManager.Instance.GetPlayer().transform.DOMove(endPos.position, 2).SetEase(Ease.InBack).OnComplete(RetryOrNextPanel);
              
                PlayerControlManager.Instance.GetPlayer().GetComponent<BoxCollider2D>().enabled = false;



            }


        }

    }


    void PlayerUpMove() // 플레이어 위로 이동 
    {
        Debug.Log("PlayerUpMove");
        Transform endPos = PlayerControlManager.Instance.GetEndPos().transform;
        PlayerControlManager.Instance.GetPlayer().transform.DOMove(endPos.position, 2).SetEase(Ease.Linear).OnComplete(RetryOrNextPanel);

    }
    void RetryOrNextPanel()
    {
        /* Retry Or Next Button */
        //CanvasManager.Instance.OpenRetryOrNextPanel();  
        if (isSceneMoved == false)
        {
            CanvasManager.Instance.ChangeScene("HE");
            Debug.Log("CanvasManager.Instance.ChangeScene : HE");
        }

        isSceneMoved = true;
    }
}
