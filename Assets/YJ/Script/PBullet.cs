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
        /* ���� */
        if (collision.gameObject.tag == "Monster") //���� ���Ϳ� �¾��� ���
        {
            //�浹�� �ı�
            if (collision.gameObject.GetComponent<Monster>() != null)
                collision.gameObject.GetComponent<Monster>().Damage(dmg);

            //���� ����
            //Destroy(collision.gameObject);

            //����Ʈ ����
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //����Ʈ 1�� �ڿ� �����
            Destroy(go, 0.5f);

            //���� �浹 �� ������
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            // ���� ������ 
            collision.GetComponent<Boss>().Damage(dmg);
            //����Ʈ ����
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //����Ʈ 1�� �ڿ� �����
            Destroy(go, 1);

            //�̻��� ����� (�ڱ��ڽ�)
            Destroy(gameObject);

        }

        /* ����  */
        if (collision.gameObject.CompareTag("HE_Enemy1")) //���� ����1�� �¾��� ���
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<HE_Enemy1>().Damage(dmg);

            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //����Ʈ 1�� �ڿ� �����
            Destroy(go, 0.5f);
        }

        if (collision.gameObject.CompareTag("HE_Enemy2")) //���� ����2�� �¾��� ���
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<HE_Enemy2>().Damage(dmg);

            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //����Ʈ 1�� �ڿ� �����
            Destroy(go, 0.5f);
        }


        /* ��ȣ  */
        if (collision.gameObject.CompareTag("DH_Boss"))
        {
            Destroy(gameObject);
            if (collision.gameObject.GetComponent<DH_Boss>() != null)
                collision.gameObject.GetComponent<DH_Boss>().Damage(dmg);

            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //����Ʈ 1�� �ڿ� �����
            Destroy(go, 0.5f);
        }

        /* â�� */
        if (collision.tag == "CMBoss")
        {
            collision.GetComponent<BossDieCheck>().Damage(gameObject.GetComponent<PBullet>().dmg, transform.position);
            Destroy(gameObject);

            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);

            //����Ʈ 1�� �ڿ� �����
            Destroy(go, 0.2f);    
        }

    }
}
