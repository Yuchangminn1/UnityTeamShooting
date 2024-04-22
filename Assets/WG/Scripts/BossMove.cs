using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class BossMove : MonoBehaviour
{
    public float BOSS_HP;
    public static bool isNeedToMove = false;
    public static bool isNeedToAttack = false;
    public static bool isBossAlive = true;
    [SerializeField] GameObject DestroyEffect;
    float Destroy_Effect_Interval = 0.1f;
    float Destroy_Effect_Timer = 0;
    bool CorisRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        //내려올땐 무적
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        BOSS_HP = GameObject.Find("GameManager").GetComponent<EnemyData>().BOSS_5_HP_p;
        transform.DOMove(new Vector2(0, 8.15f), 3f, false);
        Invoke("startfight", 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Boss HP : " + BOSS_HP);
        if (BOSS_HP <= 0)
        {
            isNeedToMove = false;
            isNeedToAttack = false;
            isBossAlive = false;
            PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = true;
            Destroy_Effect_Timer += Time.deltaTime;
            StartCoroutine(Destroy_Effect());
            if (!CorisRunning)
            {
                WG_SoundManager.instance.audioSource_Shot.volume = 10f;
                StartCoroutine(DestroySound());
            }
            if (Destroy_Effect_Timer >= 2.000f)
            {
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    Destroy(enemies[i]);
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            BOSS_HP -= GameObject.Find("GameManager").GetComponent<PaleyrData>().ATK_p; //나중에 플레이어 데미지 스크립트에서 끌어와야함
            playerLogic.score += 50;
           // isNeedToMove = false;
        }
    }
    //죽을때 탄환 레이저 검색해서 삭제
    //private static void BulletDestroyer()
    //{
    //    var bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
    //    var Lazers = GameObject.FindGameObjectsWithTag("EnemyLazer");
    //    for (int i = 0; i < bullets.Length; i++)
    //    {
    //        bullets[i].SetActive(false);
    //    }
    //    for (int j = 0; j < Lazers.Length; j++)
    //    {
    //        Lazers[j].SetActive(false);
    //    }
    //}

    IEnumerator Destroy_Effect()
    {
        //폭발 X,Y동일한 스케일로 가져가게

        float Effect_Random_Scale = UnityEngine.Random.Range(1f, 6f);
        Instantiate(DestroyEffect, new Vector2(transform.position.x + UnityEngine.Random.Range(-2f, 2f),
        transform.position.y + UnityEngine.Random.Range(-2f, 2f)), Quaternion.identity);
        DestroyEffect.transform.localScale = new Vector2(Effect_Random_Scale, Effect_Random_Scale);
        yield return new WaitForSeconds(Destroy_Effect_Interval);
    }

    IEnumerator DestroySound()
    {
        CorisRunning = true;
        while (!gameObject.IsDestroyed())
        {
            WG_SoundManager.instance.ShootingSound(1);
            yield return new WaitForSeconds(Destroy_Effect_Interval * 3f);
        }
    }

    void startfight()
    {
        //충돌 활성화
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        isNeedToAttack = true;
        isNeedToMove = true;
    }

    private void OnDestroy()
    {
        HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();

        playerLogic.score += 10000;
    }
}
