using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Boss : MonoBehaviour
{
    public GameObject Bossbullet; //���� �̻���
    public GameObject Bossbullet2;
    public GameObject Bossbullet3;
    public GameObject Bossbullet4;
    [SerializeField] GameObject DestroyEffect;

    public Transform tr; // �̻��� ��ġ
    public Transform tr2;
    public Transform tr3;

    int flag = 1;
    int speed = 9;

    public int HP = 100;
    public int MaxHp = 1000;
    public GameObject player;
    public GameObject Bullet;
    public int monsterScore;


    public int PatternIndex; //����
    public int curPatternCount; //���� ���� 
    public int[] maxPatternCount;



    public bool MoveDown = false;

    bool bossAttackPattern1Done;
    bool bossAttackPattern2Done;
    bool bossAttackPattern3Done;

    bool isSceneMoved = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.position = new Vector3(0,10.0f, 0); //ȭ�� �ۿ� ����  
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        StartCoroutine("Move");

        StartCoroutine(BossSpawn());

    }
    void Update()
    {
        //�¿�� ������
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
            yield return seconds; //���� ��ȯ

            Pattern();
        }
    }



    void Pattern()
    {
        //������ 3���� �迭 0-2
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
        //    //�̻��� 3�� �߻�
        //    Instantiate(Bossbullet, tr.position, Quaternion.identity);
        //    Instantiate(Bossbullet2, tr2.position, Quaternion.identity);
        //    Instantiate(Bossbullet3, tr3.position, Quaternion.identity);

        //    yield return new WaitForSeconds(2f);

        //}
    }


    IEnumerator BulletSecond()
    {
        Vector3 target = Vector3.zero; //��ǥ ��ġ(�߾�)


        while (true)
        {
            if(transform.position.x <= 5.0f ||
               transform.position.x >= 5.0f)
            {

                if (HP <= MaxHp * 0.5f) //ü��
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
        float attackRate = 2; //���� �ֱ�
        int count = 10; // �߻�ü ����
        float intervalAngle = 360 / count;
        float weightAngle = 0;


        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                //�߻�ü ���� ��ġ���� ����
                GameObject clone = Instantiate(Bossbullet4, transform.position, Quaternion.identity);

                //�߻�ü �̵� ����(����) 
                float angle = weightAngle + intervalAngle * i;

                //�߻�ü �̵� ���� (����)������ �������� �ٲ�
                //Cos(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                // float x = Mathf.Cos(angle * Mathf.Deg2Rad);���� �ᵵ �Ȱ���

                //sin(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                clone.GetComponent<BossBullet>().Move(new Vector2(x, y)); //�� �������� ���ư�
            }

            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1; //0������ ������ �ȵ�

            //attackRate �ð���ŭ ��� - ��� ��� �ʹ� ���� ������ ��
            yield return new WaitForSeconds(attackRate); //2�ʸ��� ���� �̻��� �߻�
        }
    }





    public void Damage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Destroy(gameObject);  // ���� �װ� 
            /* ���� ����� ����Ʈ ó�� */
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

            if (PlayerControlManager.Instance.GetPlayer() != null) //null�� �ƴϸ�
            {

                HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();
                playerLogic.score += monsterScore; //���Ͱ� �ı� �� �� �÷��̾�� ���� �߰�����  

                CanvasManager.Instance.OpenClearPanel();
                CanvasManager.Instance.OpenScorePanel();

                PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = true; // �Ѿ��� ���� ������.   
                HE_Player.JustOne = false;
                //Transform startPos = PlayerControlManager.Instance.GetStartPos().transform;  
                //PlayerControlManager.Instance.GetPlayer().transform.DOMove(startPos.position, 2).SetEase(Ease.Linear).OnComplete(PlayerUpMove);  

                Transform endPos = PlayerControlManager.Instance.GetEndPos().transform;
                PlayerControlManager.Instance.GetPlayer().transform.DOMove(endPos.position, 2).SetEase(Ease.InBack).OnComplete(RetryOrNextPanel);
              
                PlayerControlManager.Instance.GetPlayer().GetComponent<BoxCollider2D>().enabled = false;



            }


        }

    }


    void PlayerUpMove() // �÷��̾� ���� �̵� 
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
