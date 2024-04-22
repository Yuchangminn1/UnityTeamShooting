using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerMove : MonoBehaviour
{
    public float CustomTimeScale = 1.0f;
    SpriteRenderer HitBoxRenderer;
    PaleyrData playrdata;
    [SerializeField] float Speed = 2f;
    [SerializeField] GameObject[] Missile;
    [SerializeField] Transform[] MissileGen;
    [SerializeField] float bullet_Interval = 0.2f;
    Animator anim;
    float shot_time_count;
    public GameObject Fade;
    void Start()
    {
        HitBoxRenderer = GameObject.Find("HitChecker").GetComponent<SpriteRenderer>();
        playrdata = GameObject.Find("GameManager").GetComponent<PaleyrData>();
        WG_SoundManager.instance.audioSource_Shot.volume = 0.15f;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        shot_time_count += Time.deltaTime;
        if (BossMove.isBossAlive)
        {
            PlayerMoveControl();
            Shot();
        }
        else
        {
            Invoke("BeatBossMove", 0f);
        }
    }

    private void BeatBossMove()
    {
        //클리어시 시간 배속 1배속으로 초기화
        Time.timeScale = 1.0f;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();

        AfterBeat();
        transform.DOMove(new Vector2(0, transform.position.y), 2f, false);
        transform.DOMove(new Vector2(0, transform.position.y + 20f), 2f, false).SetEase(Ease.InBack).SetDelay(2f);
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
        //화면 밖으로 이동하고 스크립트 완전 종료
        gameObject.GetComponent<PlayerMove>().enabled = false;
        WG_SoundManager.instance.audioSource_Shot.volume = 1f;
        WG_SoundManager.instance.audioSource_Shot.pitch = 1f;
        WG_SoundManager.instance.ShootingSound(3);
    }

    private void Shot()
    {
        if (shot_time_count > bullet_Interval)
        {
            shot_time_count = 0;
            if (Input.GetMouseButton(0))
            {
                switch (playrdata.Power_p)
                {
                    case 1:
                        Instantiate(Missile[0], MissileGen[0].position, Quaternion.identity);
                        WG_SoundManager.instance.audioSource_Shot.pitch = 1f;
                        WG_SoundManager.instance.ShootingSound(0);
                        break;
                    case 2:
                        Instantiate(Missile[1], MissileGen[0].position, Quaternion.identity);
                        WG_SoundManager.instance.audioSource_Shot.pitch = 1.1f;
                        WG_SoundManager.instance.ShootingSound(0);
                        break;
                    case 3:
                        Instantiate(Missile[2], MissileGen[0].position, Quaternion.identity);
                        WG_SoundManager.instance.audioSource_Shot.pitch = 1.2f;
                        WG_SoundManager.instance.ShootingSound(0);
                        break;
                    case 4:
                    case 5:
                        WG_SoundManager.instance.audioSource_Shot.pitch = 0.9f;
                        WG_SoundManager.instance.ShootingSound(0);
                        Instantiate(Missile[2], MissileGen[0].position, Quaternion.identity);
                        Instantiate(Missile[2], MissileGen[1].position, Quaternion.identity);
                        Instantiate(Missile[2], MissileGen[2].position, Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void PlayerMoveControl()
    {
        float Move_X = Speed * Time.deltaTime * Input.GetAxisRaw("Horizontal") * CustomTimeScale;
        float Move_Y = Speed * Time.deltaTime * Input.GetAxisRaw("Vertical") * CustomTimeScale;
        float CurrentPos_X = transform.position.x;
        float CurrentPos_Y = transform.position.y;
        float Next_X = Mathf.Clamp(CurrentPos_X + Move_X, -5.25f, 5.25f);
        float Next_Y = Mathf.Clamp(CurrentPos_Y + Move_Y, -9.7f, 9.7f);
        transform.position = new Vector2(Next_X, Next_Y);

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

        if (Input.GetMouseButton(1))
        {
            HitBoxRenderer.enabled = true;
            Time.timeScale = 0.3f;
            CustomTimeScale = 1.3f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            HitBoxRenderer.enabled = false;
            Time.timeScale = 1;
            CustomTimeScale = 1;
        }
    }

    void AfterBeat()
    {
        if (transform.position.y >= 12f)
        {
            //페이드 아웃 연출
            //클리어 연출
            WG_SoundManager.instance.audioSource_Shot.volume = 1f;
            GameObject.Find("GameManager").GetComponent<AudioSource>().clip = WG_SoundManager.instance.Audio[7];
            GameObject.Find("GameManager").GetComponent<AudioSource>().Play();
            //WG_SoundManager.instance.ShootingSound(7);
            Fade.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            playrdata.Power_p++;
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        //버그제거
        Time.timeScale = 1.0f;
        playrdata.Power_p = 1f;

        //Retry UI
    }

}
