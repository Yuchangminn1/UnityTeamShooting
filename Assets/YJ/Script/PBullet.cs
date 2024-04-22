using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 4.0f;
    public int dmg;
    public float timeDistance;
    public GameObject effect;
    void Start()
    {
        if (PlayerControlManager.Instance.GetPlayer() != null)
        {
            PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().timeDistance = timeDistance;
        }
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* 윤주 */
        if (collision.gameObject.tag == "Monster") //윤주 몬스터에 맞았을 경우
        {
            //충돌시 파괴
            if (collision.gameObject.GetComponent<Monster>() != null)
                collision.gameObject.GetComponent<Monster>().Damage(dmg);

            //몬스터 지움
            //Destroy(collision.gameObject);

            //이펙트 생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //이펙트 1초 뒤에 지우기
            Destroy(go, 0.5f);

            //몬스터 충돌 시 지워줌
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            // 보스 데미지 
            collision.GetComponent<Boss>().Damage(dmg);
            //이펙트 생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //이펙트 1초 뒤에 지우기
            Destroy(go, 1);

            //미사일 지우기 (자기자신)
            Destroy(gameObject);

        }

        /* 하은  */
        if (collision.gameObject.CompareTag("HE_Enemy1")) //하은 몬스터1에 맞았을 경우
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<HE_Enemy1>().Damage(dmg);

            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //이펙트 1초 뒤에 지우기
            Destroy(go, 0.5f);
        }

        if (collision.gameObject.CompareTag("HE_Enemy2")) //하은 몬스터2에 맞았을 경우
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<HE_Enemy2>().Damage(dmg);

            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //이펙트 1초 뒤에 지우기
            Destroy(go, 0.5f);
        }


        /* 동호  */
        if (collision.gameObject.CompareTag("DH_Boss"))
        {
            Destroy(gameObject);
            if (collision.gameObject.GetComponent<DH_Boss>() != null)
                collision.gameObject.GetComponent<DH_Boss>().Damage(dmg);

            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //이펙트 1초 뒤에 지우기
            Destroy(go, 0.5f);
        }

        /* 창민 */
        if (collision.tag == "CMBoss")
        {
            collision.GetComponent<BossDieCheck>().Damage(gameObject.GetComponent<PBullet>().dmg, transform.position);
            Destroy(gameObject);

            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //이펙트 1초 뒤에 지우기
            Destroy(go, 0.2f);    
        }

    }
}
