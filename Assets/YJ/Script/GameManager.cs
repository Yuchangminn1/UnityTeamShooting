using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //몬스터 배열
    public GameObject[] monsters;

    //몬스터 위치 (여러개)
    public Transform[] spawnPoints;

    //소환 시간
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public Text scoreText;
    public Image[] lifeImage; //이미지 3개 배열
    public GameObject gameOver;

    public GameObject player;

    public GameObject Boss;

    [SerializeField]
    GameObject textWarning; //워닝 텍스트

    //bool swi = true;
    int myLife = 3;
    IEnumerator test;
    private void Awake()
    {
        textWarning.SetActive(false); //비활성화

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
        ////현재 흐르고 있는 시간
        //curSpawnDelay += Time.deltaTime;

        //if(curSpawnDelay > maxSpawnDelay)
        //{
        //    SpawnMonster();
        //    maxSpawnDelay = UnityEngine.Random.Range(0.5f, 3f); //랜덤하게 소환
        //    curSpawnDelay = 0; //적 생성 후 초기화
        //}

        //텍스트 UI
        //Player playerLogic = player.GetComponent<Player>();
        //scoreText.text = string.Format("{0:n0}", playerLogic.score); //세자리수 마다 , 표시  

        HE_Player playerLogic = player.GetComponent<HE_Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); //세자리수 마다 , 표시    


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
            //현재 흐르고 있는 시간
            curSpawnDelay += Time.deltaTime;

            if (curSpawnDelay > maxSpawnDelay)
            {
                SpawnMonster();
                //maxSpawnDelay = UnityEngine.Random.Range(0.5f, 3f); //랜덤하게 소환
                curSpawnDelay = 0; //적 생성 후 초기화 

            }
            yield return null;
            //Debug.Log("SpawnTest");    

        }
    }
    void SpawnMonster()
    {
        int randomMonster = UnityEngine.Random.Range(0, 2); //적 고름 2가지
        int rnadomPoint = UnityEngine.Random.Range(0, 9);//적 소환 위치

        //몬스터 소환
        GameObject monster = Instantiate(monsters[randomMonster], spawnPoints[rnadomPoint].position, Quaternion.identity);
        Monster monsterLogic = monster.GetComponent<Monster>();

        //몬스터 속도 게임매니저가 관리
        Rigidbody2D rig = monster.GetComponent<Rigidbody2D>();
        if (rnadomPoint == 5 || rnadomPoint == 6) //우측에서
        {
            rig.velocity = new Vector2(monsterLogic.speed * (-1), -1);
        }
        else if (rnadomPoint == 7 || rnadomPoint == 8) //좌측에서
        {
            rig.velocity = new Vector2(monsterLogic.speed, -1);
        }
        else //위에서 아래로
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
        //    //보스 출현
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

        //보스 출현
        textWarning.SetActive(true);

        StartCoroutine("SpawnBoss");

        StartCoroutine(DisableTextWarning()); //텍스트 비활성화
    }

    IEnumerator DisableTextWarning()
    {
        yield return new WaitForSeconds(1.0f);
        textWarning.SetActive(false);
    }


    public void UpdateLifeIcon()
    {
        // 남은 목숨 개수보다 작은 인덱스의 이미지만 보이도록 설정   
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        myLife -= 1;  

        if (myLife <= 0)
        {
            if (CanvasManager.Instance.isClearOpen == true) // 클리어 창이 떴으면   
                return;
            if (CanvasManager.Instance.GetRetryExitPanel() != null && CanvasManager.Instance.isRetryExitOpen == false)
            {
                /* TODO : 스테이지 클리어 시 Retry Exit panel 안뜨게 */
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
        // 남은 목숨 개수보다 큰 인덱스의 이미지는 투명하게 설정하여 보이지 않게 함  
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
    //            playerLogic.isDmg = false; // 초기화 
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
