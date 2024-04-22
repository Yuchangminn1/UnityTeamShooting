using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using Unity.VisualScripting;

public class BossAttack : Poolable
{
    Rigidbody2D rb;
    public GameObject GameManger;
    //FOR DEBUG
    public int PatternType_CorR = 0;
    public int PatternType_Update = 0;
    public float CircleShot1_Speed = 0.5f;
    public float Sprial_Spin1_ShotInterval = 0.05f;
    public float Sprial_Spin1_SpinSpeed = 3f;
    //코루틴 1회 체크
    bool isnowCoroutine = false;
    bool HazzlingBorn = false;
    //보스 랜덤 포지션
    float RandomX, RandomY, RandomBigBullet;
    float BOSS_HP;
    float Standard_BOSS_HP;
    // Start is called before the first frame update
    public int option = 0;
    public float Timer = 0;
    [Header("생성기 변수")]
    public float x = 1f;
    public float y = 1f;
    public float z = 1f;
    public float BulletSpeed = 10f;
    public float BulletSpeedAcceleration = 1f;
    public float BulletFireInterval = 0.03f;
    public float BulletFireInterval2 = 0.03f;
    public float angle = 0f;
    public float angle_Interval = 0f;
    public float Spin_Modificator = 1f;
    public float Spin_Rate = 1f;
    public float DoRotateAngle = 0f; //커질수록 휘어져서 뭉치는 힘 증가
    public float DoRotateDuration = 10f; // 작아 질 수록 원 반경 감소

    [Header("GameObjects")]
    public GameObject Hazzling;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Option : " + option);
        BOSS_HP = GameObject.Find("BOSS").GetComponent<BossMove>().BOSS_HP;
        Standard_BOSS_HP = GameManger.GetComponent<EnemyData>().BOSS_5_HP_p;
        Timer += Time.deltaTime;
        //  var BossScripts = Boss.GetComp    onent<BossMove>();
        RandomX = UnityEngine.Random.Range(-3.5f, 3.5f);
        RandomY = UnityEngine.Random.Range(-3.6f, 8.15f);
        RandomBigBullet = UnityEngine.Random.Range(1f, 2.5f);
        if (BossMove.isNeedToAttack)
        {
            if (!isnowCoroutine) ActBossPattern();
            if (BOSS_HP >= Standard_BOSS_HP * 0.9) option = 1;//option = 1;
            else if (BOSS_HP >= Standard_BOSS_HP * 0.8 && BOSS_HP < Standard_BOSS_HP * 0.9) option = 2;
            else if (BOSS_HP >= Standard_BOSS_HP * 0.6 && BOSS_HP < Standard_BOSS_HP * 0.8)
            {
                //option 3
                // triggerexit에 존재
            }
            else if (BOSS_HP >= Standard_BOSS_HP * 0.45 && BOSS_HP < Standard_BOSS_HP * 0.6)
            {
                option = 0;
            }
            else if (BOSS_HP <= Standard_BOSS_HP * 0.2)
            {
                option = 99;
            }
            else if (BOSS_HP <= 0) StopAllCoroutines();
        }
    }

    private void ActBossPattern()
    {
        StartCoroutine(Caller());
        StartCoroutine(Caller2());
    }
    IEnumerator OptionChanger(int a, float Timer)
    {
        yield return new WaitForSeconds(Timer);
        option = a;
    }
    void RoamingAndCircleShot()
    {
        transform.DOMove(new Vector2(RandomX, RandomY), 1f, false);
        if (Timer > 1f)
        {
            Timer = 0;
            CircleShot1();
        }

    }
    void CircleShot1()
    {
        for (int i = 0; i < 360; i += 13)
        {
            //오브젝트 풀에서 탄환 가져오기
            var BulletGo = ObjectPoolPractice.Instance.GetGo("Bullet1");
            BulletGo.transform.position = transform.position;
            var BulletGo2 = ObjectPoolPractice.Instance.GetGo("Bullet2");
            BulletGo2.transform.position = transform.position;

            BulletGo.GetComponent<EnemyBullet>().Speed = 10f;
            BulletGo.GetComponent<EnemyBullet>().Move(Vector2.down);
            BulletGo.transform.rotation = Quaternion.Euler(0, 0, i);

            BulletGo2.GetComponent<EnemyBullet>().Speed = 5f;
            BulletGo2.GetComponent<EnemyBullet>().Move(Vector2.down);
            BulletGo2.transform.rotation = Quaternion.Euler(0, 0, i + 13);
        }
        for (int i = 0; i < 360; i += 9)
        {
            var BulletGo3 = ObjectPoolPractice.Instance.GetGo("Bullet3");
            BulletGo3.transform.position = transform.position;

            BulletGo3.GetComponent<EnemyBullet>().Speed = 2f;
            BulletGo3.GetComponent<EnemyBullet>().Move(Vector2.down);
            BulletGo3.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    void DNAshot()
    {

        var Bullet1_BIG = ObjectPoolPractice.Instance.GetGo("Bullet1_BIG");
        Bullet1_BIG.transform.position = transform.position;
        var Bullet2_BIG = ObjectPoolPractice.Instance.GetGo("Bullet2_BIG");
        Bullet2_BIG.transform.position = transform.position;

        Bullet1_BIG.SetActive(true);
        Bullet2_BIG.SetActive(true);
        Bullet1_BIG.transform.localScale = new Vector2(4f, 4f);
        Bullet2_BIG.transform.localScale = new Vector2(4f, 4f);

        angle += angle_Interval;
        x = Mathf.Cos(angle * Mathf.Deg2Rad);
        y = Mathf.Sin(angle * Mathf.Deg2Rad);
        Bullet1_BIG.GetComponent<EnemyBullet>().Move(new Vector2(y, 1));
        Bullet2_BIG.GetComponent<EnemyBullet>().Move(new Vector2(-y, 1)); // DNA모양 사인파동 탄막이 나온다

        Bullet1_BIG.transform.rotation = Quaternion.Euler(0, 0, 180);
        Bullet2_BIG.transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    void BigAndShrinkEmit()
    {
        Vector2 dis = GameObject.FindGameObjectWithTag("Player_HitBox").transform.position - transform.position;
        Vector2 dir = dis.normalized;
        var BulletGo4 = ObjectPoolPractice.Instance.GetGo("Bullet4");
        BulletGo4.transform.position = transform.position;
        BulletGo4.SetActive(true);
        BulletGo4.GetComponent<EnemyBullet>().Speed = 3f;
        BulletGo4.GetComponent<EnemyBullet>().Move(dir);
        BulletGo4.transform.DOScale(RandomBigBullet, 3f).SetEase(Ease.InOutElastic);
    }
    void MoveToCenterAndHazzling()
    {
        float x = 3f, y = 3f;
        transform.DOMove(Vector2.zero, 3f, false).SetAutoKill(true);
        for (int i = 0; i < 2; i++)
        {
            GameObject Hazzlings = Instantiate(Hazzling, transform.position, Quaternion.identity);
            Hazzlings.transform.DOMove(new Vector2(x, 0), 1f, false);
            x = -x;
        }
        for (int j = 0; j < 2; j++)
        {
            GameObject Hazzlings = Instantiate(Hazzling, transform.position, Quaternion.identity);
            Hazzlings.transform.DOMove(new Vector2(0, y), 1f, false);
            y = -y;
        }

    }
    void Practice2()
    {
        var BulletGo1 = ObjectPoolPractice.Instance.GetGo("Bullet7");

        BulletGo1.transform.position = transform.position;

        BulletGo1.SetActive(true);
        angle += angle_Interval * Spin_Rate;
        angle *= Spin_Modificator;
        x = Mathf.Cos(angle * Mathf.Deg2Rad);
        y = Mathf.Sin(angle * Mathf.Deg2Rad);
        BulletGo1.GetComponent<EnemyBullet>().Speed = BulletSpeed;
        BulletGo1.GetComponent<EnemyBullet>().Lerp_Duration = 1f;
        BulletGo1.GetComponent<EnemyBullet>().Move(new Vector2(x, y) * BulletSpeedAcceleration); //반시계로 회전하는 탄막이 나온다. 
                                                                                                 //angle_Interval 커질수록 소라껍질 모양이 더 커진다
        if (angle >= Mathf.Pow(Mathf.Abs(360), 2)) angle = 0f; // 오버플로우 방지    
        BulletGo1.transform.DORotate(new Vector3(0, 0, DoRotateAngle), DoRotateDuration, RotateMode.FastBeyond360);
    }
    void Bleach()
    {
        Vector3 speed = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, Vector3.zero, ref speed, 0.05f);
        var BulletGo5 = ObjectPoolPractice.Instance.GetGo("Bullet5");
        BulletGo5.transform.position = transform.position;
        BulletGo5.SetActive(true);
        var BulletGo6 = ObjectPoolPractice.Instance.GetGo("Bullet6");
        BulletGo6.transform.position = transform.position;
        BulletGo6.SetActive(true);
        angle += angle_Interval;
        x = Mathf.Cos(angle * Mathf.Deg2Rad);
        y = Mathf.Sin(angle * Mathf.Deg2Rad);
        BulletGo5.GetComponent<EnemyBullet>().Speed = 7f;
        BulletGo6.GetComponent<EnemyBullet>().Speed = 8f;
        BulletGo5.GetComponent<EnemyBullet>().Move(new Vector2(1, y));
        BulletGo5.GetComponent<EnemyBullet>().Move(new Vector2(1, -y));
        BulletGo6.GetComponent<EnemyBullet>().Move(new Vector2(1, y));
        BulletGo6.GetComponent<EnemyBullet>().Move(new Vector2(1, -y));
        BulletGo5.transform.DORotate(new Vector3(0, 0, angle), 1f, RotateMode.Fast);
        BulletGo6.transform.DORotate(new Vector3(0, 0, angle), 1f, RotateMode.Fast); // 천본앵마냥 흩날리는 탄막이 나온다
    }
    IEnumerator Caller()
    {
        isnowCoroutine = true;
        while (true)
        {
            switch (option)
            {
                case 0:
                    BulletFireInterval = 2f;
                    BigAndShrinkEmit();
                    break;
                case 1: //DNA샷
                        //StageEnterCircleShot();
                    BulletFireInterval = 0.02f;
                    DNAshot();
                    //BulletFireInterval = 3f;
                    //BigAndShrinkEmit();
                    break;
                case 2:
                    BulletFireInterval = 1f;
                    RoamingAndCircleShot();
                    break;
                case 3:

                    break;
                case 4:
                    BulletFireInterval = 0.005f;
                    Practice2(); break;
                case 99:
                    BulletFireInterval = 1f;
                    RoamingAndCircleShot();
                    break;
            }
            yield return new WaitForSeconds(BulletFireInterval);
        }
    }
    IEnumerator Caller2()
    {
        isnowCoroutine = true;
        while (true)
        {
            switch (option)
            {
                case 0:
                    BulletFireInterval2 = 0.03f;
                    Bleach();
                    break;
                case 1:
                    BulletFireInterval2 = 3f;
                    BigAndShrinkEmit();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4: break;
                case 99:
                    BulletFireInterval2 = 0.03f;
                    Spin_Modificator = UnityEngine.Random.Range(0.5f, 20f);
                    DoRotateDuration = UnityEngine.Random.Range(1f, 20f);
                    DoRotateAngle = UnityEngine.Random.Range(180f, 1080f);
                    Practice2(); break;
            }
            yield return new WaitForSeconds(BulletFireInterval2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (!HazzlingBorn)
            {

                if (BOSS_HP >= Standard_BOSS_HP * 0.6 && BOSS_HP < Standard_BOSS_HP * 0.8)
                {
                    MoveToCenterAndHazzling();
                    HazzlingBorn = true;
                }
            }
        }
    }
}
