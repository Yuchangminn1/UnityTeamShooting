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
            //�Ѿ˸� ���� -> �÷��̾�� �÷��̾� ��ũ��Ʈ���� �����Ǳ� ������

            //GameObject.Find("HE_GameScene").gameObject.GetComponent<HE_GameManager>().UpdateLifeIcon();  
        }
    }
}
