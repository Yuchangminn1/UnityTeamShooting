using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HE_Boss : MonoBehaviour
{
    int flag = 1;
    float speed = 2;
    float Delay = 15.0f;
    public float HP = 300;

    bool BossAppear = true;
    public bool BossGetAttack = false;

    [SerializeField] GameObject Enemy2;
    [SerializeField] GameObject BossBullet;
    [SerializeField] GameObject Asteriod;
    [SerializeField] Transform pos;

    [SerializeField] int monsterScore;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject DestroyEffect;

    bool swi = true;
    bool swi2 = true;
    bool isSceneMoved = false;  
    private void Start()
    {
        WG_SoundManager.instance.audioSource_Shot.volume = 0.2f;

        StartCoroutine("LaunchEnemy");
        Invoke("Stop", Delay);

    }

    IEnumerator LaunchEnemy()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.7f);
        while (swi)
        {
            yield return waitTime;
            Instantiate(Enemy2, pos.position, Quaternion.identity);
        }
    }

    void Stop()
    {
        StopCoroutine("LaunchEnemy");
        swi = false;

        if (transform.position.y <= 6.6f)
        {
            StartCoroutine("CircleFire");
            StartCoroutine("SpawnAsteriod");
            BossGetAttack = true;
        }
    }

    IEnumerator CircleFire()
    {
        float attackRate = 3;
        int count = 25;    
        float intervalAngle = 360 / count;
        float weightAngle = 0;

        WaitForSeconds waitTime = new WaitForSeconds(attackRate);
        while (swi2)
        {
            for(int i = 0; i<count; ++i)
            {
                GameObject clone = Instantiate(BossBullet, transform.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone.GetComponent<HE_BossBullet>().Move(new Vector2(x, y));
            }
            weightAngle += 1;

            yield return waitTime;
        }
    }

    IEnumerator SpawnAsteriod()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.5f);
        while (swi2)
        {
            yield return waitTime;
            float randomX = Random.Range(-4.77f, 4.77f);
            Vector3 vec = new Vector3(randomX, 9.15f, 0f);  
            Instantiate(Asteriod, vec, Quaternion.identity);
        }
    }
    void Update()
    {
        if (BossAppear == true)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }

        if (transform.position.y <= 6.6f && BossAppear == true)
        {
            BossAppear = false;
            transform.position = new Vector2(0, 6.6f);
        }

        if (BossAppear == false)
        {
            transform.Translate(flag * speed * Time.deltaTime, 0, 0);

            if (transform.position.x >= 3.55f)
                flag *= -1;
            if (transform.position.x <= -3.55f)
                flag *= -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BossGetAttack == true)
        {
            if (collision.gameObject.CompareTag("PBullet"))
            {
                int attack = collision.gameObject.GetComponent<PBullet>().dmg;
                Damage(attack);

                GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

                //이펙트 1초 뒤에 지우기
                Destroy(go, 0.5f);
            }
        }

    }
    public void Damage(int Attack)
    {
        HP -= Attack;
        if (HP <= 0)
        {
            Destroy(gameObject);

            /* 보스 사망시 이펙트 처리 */
            WG_SoundManager.instance.audioSource_Shot.volume = 10f;

            GameObject go = Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(go, 1);
            WG_SoundManager.instance.ShootingSound(1);
            //Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
            //if (monsters.Length > 0)
            //{
            //    foreach (var m in monsters)
            //    {
            //        Destroy(m);
            //    }
            //}


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


            }
        }
    }

    void RetryOrNextPanel()
    {
        /* Retry Or Next Button */
        //CanvasManager.Instance.OpenRetryOrNextPanel();  

        if (isSceneMoved == false)
        {
            CanvasManager.Instance.ChangeScene("DH");
            Debug.Log("CanvasManager.Instance.ChangeScene : DH");
        }

        isSceneMoved = true;
    }
}