using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_BossBullet_Red : MonoBehaviour
{
    Vector2 bulletDir;
    float bulletSpeed;
    public void SetDir(Vector2 dir)
    {
        bulletDir = dir;
    }
    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyForSeconds());

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletDir * bulletSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator DestroyForSeconds()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //총알만 삭제 -> 플레이어는 플레이어 스크립트에서 삭제되기 떄문에

            //GameObject.Find("HE_GameScene").gameObject.GetComponent<HE_GameManager>().UpdateLifeIcon();  
        }
    }
}
