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
            //�Ѿ˸� ���� -> �÷��̾�� �÷��̾� ��ũ��Ʈ���� �����Ǳ� ������
            Debug.Log("lazer enter");
            //GameObject.Find("HE_GameScene").gameObject.GetComponent<HE_GameManager>().UpdateLifeIcon();
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Destroy(gameObject);
    //        //�Ѿ˸� ���� -> �÷��̾�� �÷��̾� ��ũ��Ʈ���� �����Ǳ� ������
    //        Debug.Log("lazer stay");  

    //        GameObject.Find("HE_GameScene").gameObject.GetComponent<HE_GameManager>().UpdateLifeIcon();
    //    }
    //}  
}
