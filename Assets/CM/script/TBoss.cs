using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
public class TBoss : MonoBehaviour
{
    // public int HP = 100;

    //public GameObject Explosionx;



    public GameObject Explosionx;

    public BossDieCheck Boss_1;
    public BossDieCheck Boss_2;
    public BossDieCheck Boss_3;

    public int monsterScore;
    public bool isSceneMoved = false;
    public float dieBossNum = 0;

    public bool Die = false;
    public bool AllBossDied = false;
    // Start is called before the first frame update
    void Start()
    {
        WG_SoundManager.instance.audioSource_Shot.volume = 0.2f;

        //LazerPos = GameObject.Find("LazerPoint");

    }

    // Update is called once per frame
    void Update()
    {
        if (dieBossNum == 0 && (Boss_1.Dead || Boss_2.Dead || Boss_3.Dead))
        {
            dieBossNum = 1;
        }

        if (dieBossNum == 1 && ((Boss_1.Dead && Boss_2.Dead) || (Boss_2.Dead && Boss_3.Dead) || (Boss_1.Dead && Boss_3.Dead)))
        {
            dieBossNum = 2;
        }
        if (Boss_1.Dead && Boss_2.Dead && Boss_3.Dead && AllBossDied == false)
        {
            AllBossDied = true;
            transform.GetComponentInParent<TBossMove>().enabled = false;
            transform.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
            KillBoss();

        }

    }

    void RetryOrNextPanel()
    {
        /* Retry Or Next Button */
        //CanvasManager.Instance.OpenRetryOrNextPanel();  

        if (isSceneMoved == false)
        {
            CanvasManager.Instance.ChangeScene("WG");
            Debug.Log("CanvasManager.Instance.ChangeScene : WG");
        }

        isSceneMoved = true;
    }

    IEnumerator StopS()
    {
        while (true)
        {

            float MyrandomX = Random.Range(-1f, 1.1f);
            float MyrandomY = Random.Range(-1f, 1.1f);
            Vector3 tmp = transform.position;
            tmp.x += MyrandomX;
            tmp.y += MyrandomY;

            Instantiate(Explosionx, tmp, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;

    }
    public void KillBoss()
    {
        StartCoroutine("StopS");
        Destroy(gameObject, 2f);

        if (PlayerControlManager.Instance.GetPlayer() != null) //null�� �ƴϸ�
        {

            HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();
            playerLogic.score += monsterScore; //���Ͱ� �ı� �� �� �÷��̾�� ���� �߰�����  

            CanvasManager.Instance.OpenClearPanel();
            CanvasManager.Instance.OpenScorePanel();

            PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = true; // �Ѿ��� ���� ������.   
            HE_Player.JustOne = false;

            //Transform startPos = PlayerControlManager.Instance.GetStartPos().transform;  
            //PlayerControlManager.Instance.GetPlayer().transform.DOMove(startPos.position, 2).SetEase(Ease.Linear).OnComplete(PlayerUpMove);  

            Transform endPos = PlayerControlManager.Instance.GetEndPos().transform;
            PlayerControlManager.Instance.GetPlayer().transform.DOMove(endPos.position, 2).SetEase(Ease.InBack).OnComplete(RetryOrNextPanel);


        }

    }
    //private void OnDestroy()
    //{
    //    StopCoroutine("StopS");
    //}







    //public void Damage(int attack)
    //{
    //    HP -= attack;
    //    if (HP < 0)
    //    {
    //        Instantiate(Explosionx, transform.position, Quaternion.identity);
    //        Instantiate(Item, transform.position, Quaternion.identity);
    //        Destroy(gameObject);
    //    }
    //}
}
