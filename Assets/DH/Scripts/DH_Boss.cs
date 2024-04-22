using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum DH_BossMovePattern
{
    MovePattern_1,
    MovePattern_2,
    MovePattern_3,
    None,
}
public class DH_Boss : MonoBehaviour
{

    [SerializeField] DH_BossMovePattern bossMovePattern = DH_BossMovePattern.None;
    public float HP = 1000;
    [SerializeField] GameObject DH_MonsterLeft;
    [SerializeField] GameObject DH_MonsterRight;
    [SerializeField] GameObject RedEye;

    [SerializeField] GameObject BossStartPosition;
    [SerializeField] GameObject BossEndPosition;
    [SerializeField] List<GameObject> MovePattern1;
    [SerializeField] List<GameObject> MovePattern2;
    [SerializeField] List<GameObject> MovePattern3;

    [SerializeField] GameObject BossBullet_Red;
    [SerializeField] GameObject BossBullet_Green;
    [SerializeField] GameObject BossBullet_purple;
    [SerializeField] GameObject bossBulletPos1;

    [Header("���� ���ھ� ���� ")]
    [SerializeField] int monsterScore; 
    [Space]
    [Header("���� �ؽ�Ʈ ")]
    [SerializeField] GameObject textWarning; //���� �ؽ�Ʈ    
    [Space]
    [Header("���� ���� �� ����Ʈ ")]
    [SerializeField] GameObject DestroyEffect;
    [Space]

    public float Pattern1Time = 0;
    public float Pattern2Time = 0;
    public float Pattern3Time = 0;

    float countPattern1Time;
    float countPattern2Time;
    float countPattern3Time;

    public bool bossApperDone = false;  // ���� ó�� ���� �ߴ��� 
    public bool pattern1Done = false;   // ���� ���� 1 �������� 
    public bool pattern2Done = false;   // ���� ���� 2 �������� 
    public bool pattern3Done = false;   // ���� ���� 3 �������� 

    public float bossAppearTime = 3;
    public float p1MoveTime = 3; // �̵� �ð� 
    public float p2MoveTime = 2; // �̵� �ð� 
    public float p3MoveTime = 5; // �̵� �ð� 

    bool isSceneMoved = false;

    // Start is called before the first frame update
    void Start()
    {
        WG_SoundManager.instance.audioSource_Shot.volume = 0.2f;
        textWarning.SetActive(true);
        StartCoroutine(DisableTextWarning()); //�ؽ�Ʈ ��Ȱ��ȭ     

        transform.position = BossStartPosition.transform.position;
        countPattern1Time = Pattern1Time;
        countPattern2Time = Pattern2Time;
        countPattern3Time = Pattern3Time;

        MoveBossAppear();
    }
    IEnumerator DisableTextWarning()
    {
        yield return new WaitForSeconds(1.0f);
        textWarning.SetActive(false);  
    }


    // Update is called once per frame
    void Update()
    {
        if (bossApperDone == true)
        {
            if (pattern1Done == false)
            {
                countPattern1Time -= Time.deltaTime;
                //Debug.Log($"countPattern1Time : {countPattern1Time}");

                if (countPattern1Time <= 0)
                {
                    Debug.Log("���� 1 �� ");
                    countPattern1Time = Pattern1Time;
                    MovePattern_2();
                    pattern1Done = true;
                    pattern2Done = false;
                    pattern3Done = false;
                }
            }

            if (pattern1Done == true && pattern2Done == false)
            {
                countPattern2Time -= Time.deltaTime;
                if (countPattern2Time <= 0)
                {
                    Debug.Log("���� 2 �� ");
                    countPattern2Time = Pattern2Time;
                    MovePattern_3();
                    pattern1Done = true;
                    pattern2Done = true;
                    pattern3Done = false;

                }
            }

            if (pattern1Done == true && pattern2Done == true && pattern3Done == false)
            {
                countPattern3Time -= Time.deltaTime;
                if (countPattern3Time <= 0)
                {
                    Debug.Log("���� 3 �� ");
                    countPattern3Time = Pattern3Time;
                    MovePattern_1();
                    pattern1Done = false;
                    pattern2Done = false;
                    pattern3Done = false;
                }
            }
        }


    }

    /* ���� ���� �̵� */
    void MoveBossAppear()
    {
        bossMovePattern = DH_BossMovePattern.None;

        /* itween ��� */
        //iTween.MoveTo(gameObject, iTween.Hash("position", BossEndPosition.transform.position,
        //"time", bossAppearTime, "easeType", iTween.EaseType.linear, "oncomplete", "BossApperDone"));

        /* Dotween ��� */
        transform.DOMove(BossEndPosition.transform.position, bossAppearTime).SetEase(Ease.Linear).OnComplete(BossApperDone);
    }
    IEnumerator BulletPattern1(float speed) // �̻��� ���� 1  (��ä��)
    {
        float randX = Random.Range(1.0f, 5.5f);
        float randSpeed = Random.Range(6, 10);

        if (bossMovePattern == DH_BossMovePattern.MovePattern_3)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject bullet = Instantiate(BossBullet_Green, bossBulletPos1.transform.position, Quaternion.identity);
                DH_BossBullet_Green component = bullet.GetComponent<DH_BossBullet_Green>();
                if (i == 0 || i == 5)
                {
                    component.SetDir(new Vector2(0, -1));
                    component.SetSpeed(speed);
                }
                else if (i == 1 || i == 3)
                {
                    component.SetDir(new Vector2(randX / 10, -1));
                    component.SetSpeed(speed);

                }
                else if (i == 2 || i == 4)
                {
                    component.SetDir(new Vector2(-1.0f * (randX / 10), -1));
                    component.SetSpeed(speed);

                }
                yield return new WaitForSeconds(0.4f);

            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject bullet = Instantiate(BossBullet_Green, bossBulletPos1.transform.position, Quaternion.identity);
                DH_BossBullet_Green component = bullet.GetComponent<DH_BossBullet_Green>();
                if (i == 0 || i == 3 || i == 6 || i == 9)
                {
                    component.SetDir(new Vector2(Random.Range(1.0f, 5.5f) / 10, -1));
                    component.SetSpeed(Random.Range(10, 15));
                }
                else if (i == 1 || i == 4 || i == 7)
                {
                    component.SetDir(new Vector2(randX / 10, -1));
                    component.SetSpeed(Random.Range(10, 15));

                }
                else if (i == 2 || i == 5 || i == 8)
                {
                    component.SetDir(new Vector2(-1.0f * (randX / 10), -1));
                    component.SetSpeed(Random.Range(10, 15));  

                }
                yield return new WaitForSeconds(0.4f);

            }
        }

    }


    IEnumerator BulletPattern2() // �̻��� ���� 2  (�� �� ����)
    {
        int count = 30;    //�߻�ü ���� ����
        float intervalAngle = 360 / count * 2;  //�߻�ü ������ ����
                                                //float weightAngle = 0; //���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)

        //�� ���·� ����ϴ� �߻�ü ���� (count ���� ��ŭ)

        for (int i = 0; i < count; ++i)
        {
            //�߻�ü ����
            GameObject bullet_red = Instantiate(BossBullet_Red, bossBulletPos1.transform.position, Quaternion.identity);
            DH_BossBullet_Red component = bullet_red.GetComponent<DH_BossBullet_Red>();

            //�߻�ü �̵� ����(����)
            float angle = intervalAngle * i;
            //�߻�ü �̵� ���� (����)
            //Cos(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            //Sin(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);
            //�߻�ü �̵� ���� ����
            component.SetDir(new Vector2(x, y));
            component.SetSpeed(3);

            yield return new WaitForSeconds(0.1f); //3�ʸ��� ���� �̻��� �߻�

        }
    }
    void CreateLazerBullet() //Lazer Bullet ���� 
    {
        if (DH_MonsterLeft.transform.childCount == 0)
        {
            GameObject bullet_purple_left = Instantiate(BossBullet_purple, DH_MonsterLeft.transform.position, Quaternion.identity);
            bullet_purple_left.transform.SetParent(DH_MonsterLeft.transform);
            bullet_purple_left.transform.localPosition = new Vector3(-0.01f, 0.51f, 0);

        }


        if (DH_MonsterRight.transform.childCount == 0)
        {
            GameObject bullet_purple_right = Instantiate(BossBullet_purple, DH_MonsterRight.transform.position, Quaternion.identity);
            bullet_purple_right.transform.SetParent(DH_MonsterRight.transform);
            bullet_purple_right.transform.localPosition = new Vector3(0.01f, 0.55f, 0);
        }


    }
    public void RotateTest0() // �̻��� ���� 3 (Lazar)
    {
        iTween.EaseType _easytype = (iTween.EaseType)Random.Range(0, 31);
        int randTIme = Random.Range(5, 7);

        if (DH_MonsterLeft.transform.childCount != 0 && DH_MonsterLeft.transform.GetChild(0).gameObject != null)
        {
            // ���� �����̼����� ������ ȸ������ ���,
            // "oncomplete" �ɼ��� �߰��ؼ�, �������� ������ ���ο� �������� ���� �߰�
            iTween.RotateTo(DH_MonsterLeft.transform.GetChild(0).gameObject,
              iTween.Hash(
                "z", 0,
                "time", randTIme,
                "easeType", Ease.Linear
                ));
        }

        if (DH_MonsterRight.transform.childCount != 0 && DH_MonsterRight.transform.GetChild(0).gameObject != null)
        {
            iTween.RotateTo(DH_MonsterRight.transform.GetChild(0).gameObject,
                iTween.Hash(
                    "z", 0,
                    "time", randTIme,
                    "easeType", Ease.Linear
                ));
        }

    }
    public void RotateTest1() // �̻��� ���� 3 (Lazar)
    {
        iTween.EaseType _easytype = (iTween.EaseType)Random.Range(0, 31);
        int randTIme = Random.Range(5, 7);
        int randAngle = Random.Range(5, 10);

        if (DH_MonsterLeft.transform.childCount != 0 && DH_MonsterLeft.transform.GetChild(0).gameObject != null)
        {
            // ���� �����̼����� ������ ȸ������ ���,
            // "oncomplete" �ɼ��� �߰��ؼ�, �������� ������ ���ο� �������� ���� �߰�
            iTween.RotateTo(DH_MonsterLeft.transform.GetChild(0).gameObject,
              iTween.Hash(
                "z", randAngle,
                "time", randTIme,
                "easeType", Ease.InElastic
                ));
        }

        if (DH_MonsterRight.transform.childCount != 0 && DH_MonsterRight.transform.GetChild(0).gameObject != null)
        {
            iTween.RotateTo(DH_MonsterRight.transform.GetChild(0).gameObject,
                iTween.Hash(
                    "z", -randAngle,
                    "time", randTIme,
                    "easeType", Ease.InElastic
                ));
        }

    }
    public void RotateTest2() // �̻��� ���� 3 (Lazar)
    {
        iTween.EaseType _easytype = (iTween.EaseType)Random.Range(0, 31);
        int randTIme = Random.Range(3, 6);

        if (DH_MonsterLeft.transform.childCount != 0 && DH_MonsterLeft.transform.GetChild(0).gameObject != null)
        {
            int randAngle = Random.Range(10, 40);

            iTween.RotateTo(DH_MonsterLeft.transform.GetChild(0).gameObject,
                iTween.Hash(
                "z", -randAngle,
                "time", randTIme,
                "easeType", Ease.InElastic
                ));
        }

        if (DH_MonsterRight.transform.childCount != 0 && DH_MonsterRight.transform.GetChild(0).gameObject != null)
        {
            int randAngle = Random.Range(10, 40);

            iTween.RotateTo(DH_MonsterRight.transform.GetChild(0).gameObject,
                iTween.Hash(
                    "z", randAngle,
                    "time", randTIme,
                    "easeType", Ease.InElastic
                    ));
        }


    }

    void BossApperDone()
    {
        bossApperDone = true;
        MovePattern_1();
    }

    /* �̵� ���� 1��  */
    void MovePattern_1()
    {
        bossMovePattern = DH_BossMovePattern.MovePattern_1;
        RedEye.SetActive(false);
        /* 3��° ���� �ʱ�ȭ */
        DH_MonsterLeft.SetActive(false);
        DH_MonsterRight.SetActive(false);

        foreach (Transform child in DH_MonsterLeft.transform)
        {
            if (child != null)
                Destroy(child.gameObject);
        }
        foreach (Transform child in DH_MonsterRight.transform)
        {
            if (child != null)
                Destroy(child.gameObject);
        }

        isFastBulletsActivate = false;

        MoveObj1_ver1();
    }
    void MoveObj1_ver1()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern1[0].transform.position,
        "time", p1MoveTime, "easeType", iTween.EaseType.linear, "oncomplete", "MoveObj2_ver1"));

        /* Dotween ��� */
        //transform.DOMove(MovePattern1[0].transform.position, p1MoveTime).SetEase(Ease.Linear).OnComplete(MoveObj2_ver1);

        //StopAllCoroutines();
        //StartCoroutine(attackPattern1);  
        StartCoroutine(BulletPattern1(0));

    }


    void MoveObj2_ver1()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern1[1].transform.position,
        "time", p1MoveTime, "easeType", iTween.EaseType.linear, "oncomplete", "MoveObj3_ver1"));

        /* Dotween ��� */
        //transform.DOMove(MovePattern1[1].transform.position, p1MoveTime).SetEase(Ease.Linear).OnComplete(MoveObj3_ver1);

        //StopAllCoroutines();
        //StartCoroutine(attackPattern1);

        StartCoroutine(BulletPattern1(0));

    }
    void MoveObj3_ver1()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern1[2].transform.position,
        "time", p1MoveTime, "easeType", iTween.EaseType.linear, "oncomplete", "MoveObj1_ver1"));

        /* Dotween ��� */
        //transform.DOMove(MovePattern1[2].transform.position, p1MoveTime).SetEase(Ease.Linear).OnComplete(MoveObj1_ver1);

        //StopAllCoroutines();
        //StartCoroutine(attackPattern1);
        StartCoroutine(BulletPattern1(0));
    }


    /* �̵� ���� 2��*/
    void MovePattern_2()
    {
        bossMovePattern = DH_BossMovePattern.MovePattern_2;
        DH_MonsterLeft.SetActive(false);
        DH_MonsterRight.SetActive(false);

        MoveObj1_ver2();
    }
    void MoveObj1_ver2()
    {

        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern2[0].transform.position,
        "time", p2MoveTime, "easeType", iTween.EaseType.easeInBounce, "oncomplete", "InvokeMoveObj2_ver2"));

        /* Dotween ��� */
        //transform.DOMove(MovePattern2[0].transform.position, p2MoveTime).SetEase(Ease.InBounce).OnComplete(InvokeMoveObj2_ver2);


    }
    void InvokeMoveObj2_ver2()
    {
        RedEye.SetActive(true);
        StartCoroutine(BulletPattern2());
        //StopAllCoroutines();
        //StartCoroutine(attackPattern2);
        Invoke("MoveObj2_ver2", 1.5f);

    }

    void MoveObj2_ver2()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern2[1].transform.position,
        "time", p2MoveTime, "easeType", iTween.EaseType.linear, "oncomplete", "MoveObj3_ver2"));

        /* dotween ��� */
        //transform.DOMove(MovePattern2[1].transform.position, p2MoveTime).SetEase(Ease.Linear).OnComplete(MoveObj3_ver2);

    }
    void MoveObj3_ver2()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern2[2].transform.position,
        "time", p2MoveTime, "easeType", iTween.EaseType.easeInBounce, "oncomplete", "InvokeMoveObj4_ver2"));

        /* dotween ��� */
        //transform.DOMove(MovePattern2[2].transform.position, p2MoveTime).SetEase(Ease.InBounce).OnComplete(InvokeMoveObj4_ver2);  


    }
    void InvokeMoveObj4_ver2()
    {
        StartCoroutine(BulletPattern2());
        //StopAllCoroutines();
        //StartCoroutine(attackPattern2);
        Invoke("MoveObj4_ver2", 1.5f);

    }

    void MoveObj4_ver2()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern2[3].transform.position,
        "time", p2MoveTime, "easeType", iTween.EaseType.linear, "oncomplete", "MoveObj1_ver2"));

        /* doween ��� */
        //transform.DOMove(MovePattern2[3].transform.position, p2MoveTime).SetEase(Ease.Linear).OnComplete(MoveObj1_ver2);

    }

    /* �̵� ���� 3 */
    IEnumerator CoCreatePattern3()
    {
        yield return new WaitForSeconds(3.5f);
        CreateLazerBullet();
    }
    void MovePattern_3()
    {
        bossMovePattern = DH_BossMovePattern.MovePattern_3;
        DH_MonsterLeft.SetActive(true);
        DH_MonsterRight.SetActive(true);
        //CreateLazerBullet();
        StartCoroutine(CoCreatePattern3());

        MoveObj1_ver3();
    }

    bool isFastBulletsActivate = false;
    void MoveObj1_ver3()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern3[0].transform.position,
        "time", p3MoveTime, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "MoveObj2_ver3"));

        /* dotween ��� */
        //transform.DOMove(MovePattern3[0].transform.position, p3MoveTime).SetEase(Ease.Linear).OnComplete(MoveObj2_ver3);

        RotateTest0();  // 3�� ��ġ���� ���� 
        if (isFastBulletsActivate == true)
            StartCoroutine(CoFastBullets());


    }
    IEnumerator CoFastBullets()
    {
        int[] dirX = new int[] { -1, 0, 1 };
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = Instantiate(BossBullet_Green, bossBulletPos1.transform.position, Quaternion.identity);
            bullet.transform.localScale = new Vector3(1f, 1f, 0);
            DH_BossBullet_Green component = bullet.GetComponent<DH_BossBullet_Green>();
            float randX = Random.Range(1.0f, 3f);
            int randIndex = Random.Range(0, dirX.Length);
            float randSpeed = Random.Range(15, 20);  

            component.SetDir(new Vector2(dirX[randIndex] * (randX / 10), -1));
            component.SetSpeed(randSpeed);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void MoveObj2_ver3()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern3[1].transform.position,
        "time", p3MoveTime, "easeType", iTween.EaseType.linear, "oncomplete", "MoveObj3_ver3"));

        RedEye.SetActive(false);

        RotateTest1();  // 1�� ��ġ���� ���� 
        /* dotween ��� */
        //transform.DOMove(MovePattern3[1].transform.position, p3MoveTime).SetEase(Ease.Linear).OnComplete(MoveObj1_ver3);
        isFastBulletsActivate = true;

    }
    void MoveObj3_ver3()
    {
        /* itween ��� */
        iTween.MoveTo(gameObject, iTween.Hash("position", MovePattern3[2].transform.position,
        "time", p3MoveTime, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "MoveObj1_ver3"));

        RotateTest2();   // 2�� ��ġ���� ���� 
        StartCoroutine(BulletPattern1(Random.Range(10, 15)));    

    }


    /* ���� ������ */
    public void Damage(int Attack)
    {
        HP -= Attack;
        if (HP <= 0)
        {
            Destroy(gameObject);

            /* ���� ����� ����Ʈ ó�� */
            GameObject go = Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            WG_SoundManager.instance.audioSource_Shot.volume = 10f;
            WG_SoundManager.instance.ShootingSound(1);
            Destroy(go, 1);

            //Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
            //if (monsters.Length > 0)
            //{
            //    foreach (var m in monsters)
            //    {
            //        Destroy(m);
            //    }
            //}


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


            }
        }
    }

    void RetryOrNextPanel()
    {
        /* Retry Or Next Button */
        //CanvasManager.Instance.OpenRetryOrNextPanel();  

        if (isSceneMoved == false)
        {
            CanvasManager.Instance.ChangeScene("CM");
            Debug.Log("CanvasManager.Instance.ChangeScene : CM");  
        }

        isSceneMoved = true;  
    }
}
