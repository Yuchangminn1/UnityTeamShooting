using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideBullet : MonoBehaviour
{
    CameraShake shake;
    bool isShake_Cor_Excuting = false;
    [SerializeField] GameObject Warning; // ���� �˶� X1��
    GameObject Warning_Clone;
    bool isWarningEnd = true;
    [SerializeField] GameObject Lazer_Horizon; //���� �˶� X0.3����
    GameObject Lazer_Horizon_Clone;
    [SerializeField] Transform[] FireLocation;
    public float WarningAndLazer_Interval = 2f;
    int Random_Location;
    int temp_random;
    float Timer = 0;
    //waring ���� 
    public float Timer_interval = 5f;

    //���׿�
    [SerializeField] GameObject[] Meteors; //���� �˶� X0.3����

    //����
    float BOSS_HP;
    //���ӸŴ���
    public GameObject GameManger;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Meteor_Maker(10f));
        shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("BOSS") != null)
            BOSS_HP = GameObject.Find("BOSS").GetComponent<BossMove>().BOSS_HP;
        var Standard_BOSS_HP = GameManger.GetComponent<EnemyData>().BOSS_5_HP_p;
        if (!BossMove.isBossAlive)
            StopAllCoroutines();
        Random_Location = Random.Range(10, 16); // 10~15
        Timer += Time.deltaTime;
        if (Timer >= Timer_interval)
        {
            Timer = 0f;
            // �ʱ� ����
            if (BOSS_HP <= Standard_BOSS_HP * 0.7)
                WarningAndLazerCoroutine();
        }
    }

    private void WarningAndLazerCoroutine()
    {
        StartCoroutine(WarningAndLazer());
    }

    private IEnumerator WarningAndLazer()
    {
        if (BossMove.isBossAlive)
        {
            temp_random = Random_Location;

            Warning_Clone = Instantiate(Warning, new Vector2(0f, FireLocation[temp_random].position.y), Quaternion.identity);
            Warning_Clone.SetActive(false);

            for (int i = 0; i < 5; i++) // 0.1*2*i+�ý��� �����ð���ŭ �ɸ��� Waring������
                                        // 1�ʺ��� Ŀ�� Destroy�� ������Ʈ ���� �μ����� ��������
            {
                Warning_Clone.SetActive(true);
                Warning_Clone.GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                Warning_Clone.GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
            Destroy(Warning_Clone);
            isWarningEnd = true;
            //������ ��������� ���ð�
            yield return new WaitForSeconds(WarningAndLazer_Interval);


            if (isWarningEnd) //�˶��� �������� ������ �߻�
            {
                //������ ����
                Lazer_Horizon_Clone = Instantiate(Lazer_Horizon, new Vector2(0.3f, FireLocation[temp_random].position.y), Quaternion.Euler(0, 0, -90f));
                Lazer_Horizon_Clone.SetActive(false);
                //�˶� ������ ������ ������ ���� ����
                if (!isShake_Cor_Excuting)
                {
                    Lazer_Horizon_Clone.SetActive(true);
                    shake.StartShaking_CorRoutine();
                    isShake_Cor_Excuting = true;
                }
                if (isShake_Cor_Excuting)
                {
                    StartCoroutine(Lazer_Clear(1.5f));
                    isShake_Cor_Excuting = false;
                }
            }
            isWarningEnd = false;
        }
    }

    IEnumerator Lazer_Clear(float LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(Lazer_Horizon_Clone);
        shake.StopShaking();
        isShake_Cor_Excuting = false; //�ٽ� �������� false�� ����
    }

    IEnumerator Meteor_Maker(float Interval)
    {
        while (true)
        {
            int RandomMeteor = Random.Range(0, Meteors.Length);
            Instantiate(Meteors[Random.Range(0, Meteors.Length)], FireLocation[Random.Range(0, 6)].position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
            switch (RandomMeteor)
            {
                case 0: Meteor.MeteorType = "Big"; break;
                case 1: Meteor.MeteorType = "Wide"; break;
            }
            yield return new WaitForSeconds(Interval);
        }
    }
}
