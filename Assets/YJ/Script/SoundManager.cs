using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;


    AudioSource BgmPlayer;
    AudioSource SfxPlayer; //ȿ���� ���ÿ� ������ ��� �����ϹǷ� �迭

    public AudioClip[] Audioclips;


    private void Awake()
    {
        SfxPlayer = GameObject.Find("SfxPlayer").GetComponent<AudioSource>();
        BgmPlayer = GameObject.Find("BgmPlayer").GetComponent<AudioSource>();


        if (instance == null) //���� ����
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject); // �� ��ȯ �ŵ� �ʱ�ȭ���� �ʰ� ���� 
    }

    public void PlaySound(string type)
    {
        int index = 0;
        switch (type)
        {
            case "Destroy": index = 0; break;
            case "Option1": index = 1; break;
            case "Option2": index = 2; break;
            case "Option3": index = 3; break;
        }
        SfxPlayer.clip = Audioclips[index];
        SfxPlayer.Play();

    }
}
