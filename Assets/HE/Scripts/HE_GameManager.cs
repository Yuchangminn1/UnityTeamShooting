using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HE_GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image[] lifeImage; //�̹��� 3�� �迭

    [SerializeField]
    GameObject textWarning; //���� �ؽ�Ʈ    

    int myLife = 3;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControlManager.Instance.GetPlayer().transform.position = PlayerControlManager.Instance.GetStartPos().transform.position;
        PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = false;
        if(CanvasManager.Instance.GetStageLevel() == StageLevel.YJ || CanvasManager.Instance.GetStageLevel() == StageLevel.HE)
        {
            PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().power = 0;

        }else if(CanvasManager.Instance.GetStageLevel() == StageLevel.DH || CanvasManager.Instance.GetStageLevel() == StageLevel.CM)
        {
            PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().power = 2;
        }
        PlayerControlManager.Instance.GetPlayer().gameObject.SetActive(true);
        CanvasManager.Instance.isRetryExitOpen = false;
        CanvasManager.Instance.isClearOpen = false;  


    }

    // Update is called once per frame
    void Update()
    {
        HE_Player playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); //���ڸ��� ���� , ǥ��      
    }

    public void UpdateLifeIcon()
    {
        // ���� ��� �������� ���� �ε����� �̹����� ���̵��� ����   
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        myLife -= 1;

        if (myLife <= 0)
        {
            if (CanvasManager.Instance.isClearOpen == true) // Ŭ���� â�� ������   
                return;

            if (CanvasManager.Instance.GetRetryExitPanel() != null && CanvasManager.Instance.isRetryExitOpen == false)
            {
                /* TODO : �������� Ŭ���� �� Retry Exit panel �ȶ߰� */
                if (PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear == true)  
                    return;

                if (textWarning.activeSelf == false)
                    Time.timeScale = 0;

                Debug.Log("11");
                PlayerControlManager.Instance.GetPlayer().SetActive(false);
                CanvasManager.Instance.OpenRetryExitPanel();
                return;

            }

        }
        // ���� ��� �������� ū �ε����� �̹����� �����ϰ� �����Ͽ� ������ �ʰ� ��  
        for (int index = 0; index < myLife; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }
}
