using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_SoundManager : MonoBehaviour
{
    public static WG_SoundManager instance;

    public AudioClip[] Audio;
    // 0 = �÷��̾� ����      1 = ���� ����       2 = BGM      3 = �¸�      4 = ���ӿ���       5 = ���� ���� �˶�        6 = �ܺ� ������ �߻� �Ҹ�
    // 7 = ���� Ŭ���� �Ҹ�

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
