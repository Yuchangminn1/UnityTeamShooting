using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE_Player : MonoBehaviour
{
    public float Speed = 3f;
    Animator anim;

    [SerializeField]
    GameObject[] bullet;
    [SerializeField]
    Transform pos;

    public int power = 0;
    public int score;

    public bool StageClear = false;
    public static bool JustOne = false;
    public float timeDistance = 0.2f;
    void Start()
    {
        WG_SoundManager.instance.audioSource_Shot.volume = 0.15f;
        anim = GetComponent<Animator>();
        InvokeRepeating("CreatBullet", 0, timeDistance);
    }
    void CreatBullet()
    {
        if (StageClear)
        {
            if (CanvasManager.Instance.GetStageLevel() != StageLevel.WG)
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
            if (!JustOne)
            {
                JustOne = true;
                WG_SoundManager.instance.audioSource_Shot.volume = 1f;
                WG_SoundManager.instance.ShootingSound(3);
            }
            return;
        }
        if (CanvasManager.Instance.GetStageLevel() != StageLevel.WG)
            WG_SoundManager.instance.ShootingSound(0);
        if (StageClear == false && PlayerControlManager.Instance.GetPlayer().activeSelf == true)
        {
            if (CanvasManager.Instance.GetStageLevel() == StageLevel.YJ)
            {
                GameObject monster = GameObject.FindGameObjectWithTag("Monster");

                if (monster != null)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);
                }
                GameObject boss = GameObject.FindGameObjectWithTag("Boss");

                if (boss != null && boss.GetComponent<Boss>().MoveDown == true)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);
                }
            }
            else if (CanvasManager.Instance.GetStageLevel() == StageLevel.HE)
            {
                Instantiate(bullet[power], pos.position, Quaternion.identity);
            }
            else if (CanvasManager.Instance.GetStageLevel() == StageLevel.DH)
            {
                GameObject DH_boss = GameObject.FindGameObjectWithTag("DH_Boss");
                if (DH_boss != null && DH_boss.GetComponent<DH_Boss>().bossApperDone == true)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);
                }
            }
            else if (CanvasManager.Instance.GetStageLevel() == StageLevel.CM)
            {
                Instantiate(bullet[power], pos.position, Quaternion.identity);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear == true)
            return;

        float moveX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") <= 0.1f)
            anim.SetBool("left", true);
        else
            anim.SetBool("left", false);

        if (Input.GetAxis("Horizontal") >= 0.1f)
            anim.SetBool("right", true);
        else
            anim.SetBool("right", false);

        if (Input.GetAxis("Horizontal") == 0f)
            anim.SetBool("left", false);

        if (Input.GetAxis("Horizontal") == 0f)
            anim.SetBool("right", false);

        transform.Translate(moveX, moveY, 0);

        //Player�̵�����
        if (transform.position.x >= 5.0f)
            transform.position = new Vector3(5.0f, transform.position.y, 0);
        if (transform.position.x <= -5.0f)
            transform.position = new Vector3(-5.0f, transform.position.y, 0);
        if (transform.position.y >= 9.3f)
            transform.position = new Vector3(transform.position.x, 9.3f, 0);
        if (transform.position.y <= -9.3f)
            transform.position = new Vector3(transform.position.x, -9.3f, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            power += 1;
            if (power >= 3)
                power = 2;

            Destroy(collision.gameObject);
        }

    }
    private void OnDisable()
    {
        if (CanvasManager.Instance.GetStageLevel() != StageLevel.WG)
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();

    }
}

