using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMOnBecamDes : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 7f);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Finish")
        //{
        //    Time.timeScale = 0;
        //}

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //총알만 삭제 -> 플레이어는 플레이어 스크립트에서 삭제되기 떄문에

            //GameObject.Find("HE_GameScene").gameObject.GetComponent<HE_GameManager>().UpdateLifeIcon();  
        }
    }
}
