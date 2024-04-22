using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzling1 : MonoBehaviour
{
    public GameObject Explosion;
    GameObject RotateTarget;
    public Transform[] fireLocation;
    int RandomLocation;
    float RandomTimer;
    float HP;
    void Start()
    {
        HP = GameObject.Find("GameManager").GetComponent<EnemyData>().Hazzling1_HP_p;
        RotateTarget = GameObject.Find("BOSS");
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        RandomTimer += Time.deltaTime;
        if (RandomTimer > 2f)
        {
            //0 = E
            //1 = W
            //2 = N
            //3 = S
            RandomTimer = 0;
            RandomLocation = Random.Range(0, 4);
        }



    }
    IEnumerator Fire()
    {
        while (true)
        {
            var BulletGo1 = ObjectPoolPractice.Instance.GetGo("Bullet1");
            BulletGo1.transform.position = fireLocation[RandomLocation].transform.position;
            BulletGo1.SetActive(true);
            BulletGo1.GetComponent<EnemyBullet>().Speed = 5f;
            switch (RandomLocation)
            {
                case 0: BulletGo1.GetComponent<EnemyBullet>().Move(Vector2.right); break;
                case 1: BulletGo1.GetComponent<EnemyBullet>().Move(Vector2.left); break;
                case 2: BulletGo1.GetComponent<EnemyBullet>().Move(Vector2.up); break;
                case 3: BulletGo1.GetComponent<EnemyBullet>().Move(Vector2.down); break;
            }
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            HP -= GameObject.Find("GameManager").GetComponent<PaleyrData>().ATK_p;
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
 
        }
    }

    private void OnDestroy()
    {
        HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();
        playerLogic.score += 500;
        StopAllCoroutines();
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
}
