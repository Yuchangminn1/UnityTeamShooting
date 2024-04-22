using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_BossBullet_Purple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //총알만 삭제 -> 플레이어는 플레이어 스크립트에서 삭제되기 떄문에
            Debug.Log("lazer enter");
            //GameObject.Find("HE_GameScene").gameObject.GetComponent<HE_GameManager>().UpdateLifeIcon();
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Destroy(gameObject);
    //        //총알만 삭제 -> 플레이어는 플레이어 스크립트에서 삭제되기 떄문에
    //        Debug.Log("lazer stay");  

    //        GameObject.Find("HE_GameScene").gameObject.GetComponent<HE_GameManager>().UpdateLifeIcon();
    //    }
    //}  
}
