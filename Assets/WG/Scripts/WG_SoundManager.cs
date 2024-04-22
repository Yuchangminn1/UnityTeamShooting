using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_SoundManager : MonoBehaviour
{
    public static WG_SoundManager instance;

    public AudioClip[] Audio;
    // 0 = 플레이어 공격      1 = 보스 폭발       2 = BGM      3 = 승리      4 = 게임오버       5 = 보스 등장 알람        6 = 외부 레이저 발사 소리
    // 7 = 최종 클리어 소리

    public AudioSource audioSource_Shot;
    // 
    private void Awake()
    {
        if (WG_SoundManager.instance == null) WG_SoundManager.instance = this;

    }
    void Start()
    {
        audioSource_Shot = GetComponent<AudioSource>();

    }
    public void ShootingSound(int number)
    {
        audioSource_Shot.PlayOneShot(Audio[number]);
    }
    public void StopAllSound()
    {
        audioSource_Shot.Stop();
    }
    //public GameObject GetPlayer()
    //{
    //    return this.gameObject.transform.GetChild(0).gameObject;
    //}
}
