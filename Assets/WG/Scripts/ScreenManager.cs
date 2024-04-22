using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject[] Objects;
    // 0 = BOSS
    // Update is called once per frame]

    public void Start()
    {
        PlayerControlManager.Instance.GetPlayer().SetActive(false); 
    }
    void Update()
    {
        if (Objects[0].IsDestroyed())
        {
            var Bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            var Lazers = GameObject.FindGameObjectsWithTag("EnemyLazer");

            for(int i = 0; i< Bullets.Length; i++) Bullets[i].SetActive(false);
            for(int j = 0; j<Lazers.Length; j++) Destroy(Lazers[j],2f);
        }
    }
}
