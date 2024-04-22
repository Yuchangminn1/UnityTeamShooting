using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    int Score;
    HE_Player playerLogic;
    //점수
    public Text ScoreText;
    void Start()
    {
        playerLogic = PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>();
        
        Debug.Log(Score);
    }

    // Update is called once per frame
    void Update()
    {
        Score = playerLogic.score;
        ScoreText.text = string.Format("{0:n0}", playerLogic.score); //세자리수 마다 , 표시      

    }
}
