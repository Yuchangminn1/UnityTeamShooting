using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //���� �迭
    public GameObject[] monsters;

    //���� ��ġ (������)
    public Transform[] spawnPoints;

    //��ȯ �ð�
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public Text scoreText;
    public Image[] lifeImage; //�̹��� 3�� �迭
    public GameObject gameOver;

    public GameObject player;

    public GameObject Boss;

    [SerializeField]
    GameObject textWarning; //���� �ؽ�Ʈ

    //bool swi = true;
    int myLife = 3;
    IEnumerator test;
    private void Awake()
    {
        textWarning.SetActive(false); //��Ȱ��ȭ

    }

    void Start()
    {
        //StartCoroutine("SpawnMonster");
        //SpawnMonster();
        test = SpawnTest();
        StartCoroutine(test);
        //StartCoroutine("SpawnTest");

        Invoke("Stop", 10f);
        PlayerControlManager.Instance.GetPlayer().transform.position = PlayerControlManager.Instance.GetStartPos().transform.position;
        PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = false;
        player = PlayerControlManager.Instance.GetPlayer();
        player.GetComponent<HE_Player>().power = 0;
        PlayerControlManager.Instance.SetActivePlayer(true);
        PlayerControlManager.Instance.GetPlayer().GetComponent<BoxCollider2D>().enabled = true;  

        CanvasManager.Instance.isRetryExitOpen = false;
        CanvasManager.Instance.isClearOpen = false;

    }

    void Update()
    {
        ////���� �帣�� �ִ� �ð�
        //curSpawnDelay += Time.deltaTime;

        //if(curSpawnDelay > maxSpawnDelay)
        //{
        //    SpawnMonster();
        //    maxSpawnDelay = UnityEngine.Random.Range(0.5f, 3f); //�����ϰ� ��ȯ
        //    curSpawnDelay = 0; //�� ���� �� �ʱ�ȭ
        //}

        //�ؽ�Ʈ UI
        //Player playerLogic = player.GetComponent<Player>();
        //scoreText.text = string.Format("{0:n0}", playerLogic.score); //���ڸ��� ���� , ǥ��  

        HE_Player playerLogic = player.GetComponent<HE_Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); //���ڸ��� ���� , ǥ��    


    }
    //IEnumerator SpawnMonster()
    //{
    //    Spawn();
    //    yield return null;
    //}

    IEnumerator SpawnTest()
    {
        while (true)
        {
            //���� �帣�� �ִ� �ð�
            curSpawnDelay += Time.deltaTime;

            if (curSpawnDelay > maxSpawnDelay)
            {
                SpawnMonster();
                //maxSpawnDelay = UnityEngine.Random.Range(0.5f, 3f); //�����ϰ� ��ȯ
                curSpawnDelay = 0; //�� ���� �� �ʱ�ȭ 

            }
            yield return null;
            //Debug.Log("SpawnTest");    

        }
    }
    void SpawnMonster()
    {
        int randomMonster = UnityEngine.Random.Range(0, 2); //�� �� 2����
        int rnadomPoint = UnityEngine.Random.Range(0, 9);//�� ��ȯ ��ġ

        //���� ��ȯ
        GameObject monster = Instantiate(monsters[randomMonster], spawnPoints[rnadomPoint].position, Quaternion.identity);
        Monster monsterLogic = monster.GetComponent<Monster>();

        //���� �ӵ� ���ӸŴ����� ����
        Rigidbody2D rig = monster.GetComponent<Rigidbody2D>();
        if (rnadomPoint == 5 || rnadomPoint == 6) //��������
        {
            rig.velocity = new Vector2(monsterLogic.speed * (-1), -1);
        }
        else if (rnadomPoint == 7 || rnadomPoint == 8) //��������
        {
            rig.velocity = new Vector2(monsterLogic.speed, -1);
        }
        else //������ �Ʒ���
        {
            rig.velocity = new Vector2(0, monsterLogic.speed * (-1));
        }
    }
    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(1.0f);

        Vector3 pos = new Vector2(0, 3.0f);
        Instantiate(Boss, pos, Quaternion.identity);

        //while (true)
        //{
        //    //���� ����
        //    Vector3 pos = new Vector2(0, 3.0f);
        //    Instantiate(Boss, pos, Quaternion.identity);
        //}
    }

    void Stop()
    {
        //swi = false;
        //StopCoroutine("SpawnMonster");
        //StopCoroutine("SpawnTest");
        //if(test !=null)
        //    StopCoroutine(test);

        StopAllCoroutines();

        Debug.Log("stop");

        //���� ����
        textWarning.SetActive(true);

        StartCoroutine("SpawnBoss");

        StartCoroutine(DisableTextWarning()); //�ؽ�Ʈ ��Ȱ��ȭ
    }

    IEnumerator DisableTextWarning()
    {
        yield return new WaitForSeconds(1.0f);
        textWarning.SetActive(false);
    }


    public void UpdateLifeIcon()
    {
        // ���� ��� �������� ���� �ε����� �̹����� ���̵��� ����   
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        myLife -= 1;  

        if (myLife <= 0)
        {
            if (CanvasManager.Instance.isClearOpen == true) // Ŭ���� â�� ������   
                return;
            if (CanvasManager.Instance.GetRetryExitPanel() != null && CanvasManager.Instance.isRetryExitOpen == false)
            {
                /* TODO : �������� Ŭ���� �� Retry Exit panel �ȶ߰� */
                if (PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear == true)
                    return;

                if (textWarning.activeSelf == false)
                    Time.timeScale = 0;

                Debug.Log("11");
                PlayerControlManager.Instance.GetPlayer().SetActive(false);
                CanvasManager.Instance.OpenRetryExitPanel();
                return;

            }

        }
        // ���� ��� �������� ū �ε����� �̹����� �����ϰ� �����Ͽ� ������ �ʰ� ��  
        for (int index = 0; index < myLife; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    //void RespawnPlayerExe()
    //{
    //    if (player != null)
    //    {
    //        player.transform.position = Vector3.down * 3.5f;
    //        player.SetActive(true);

    //        Player playerLogic = player.GetComponent<Player>();
    //        if (playerLogic != null)
    //        {
    //            playerLogic.isDmg = false; // �ʱ�ȭ 
    //        }
    //    }
    //}

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void GameReplay()
    {
        SceneManager.LoadScene(0);
    }
}
