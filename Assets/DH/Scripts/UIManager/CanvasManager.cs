using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StageLevel
{
    YJ = 1,
    HE = 2,
    DH = 3,
    CM = 4,
    WG = 5,
}
public class CanvasManager : MonoBehaviour
{
    private static CanvasManager instance;
    [SerializeField] StageLevel stageLevel;
    public CanvasGroup Fade_img;
    float fadeDuration = 1f; //�����Ǵ� �ð�  

    public GameObject LoadingUI;
    public GameObject LoadingImg;
    public TextMeshProUGUI Loading_text; //�ۼ�Ʈ ǥ���� �ؽ�Ʈ
    public Image ProgressBar;

    /* �������� ���� Text */
    [SerializeField] Text StageLevelText;
    /* Retry & Next Panel , Buttons */
    [SerializeField] GameObject RetryNextPanel;
    [SerializeField] Button ReplayButton1;
    [SerializeField] Button NextButton;

    /* Retry & Exit Panel, Buttons */
    [SerializeField] GameObject RetryExitPanel;
    [SerializeField] Button ReplayButton2;
    [SerializeField] Button ExitButton;
    public bool isRetryExitOpen = false;

    /* Score Panel , Score Text*/
    [SerializeField] GameObject ScorePanel;
    [SerializeField] Text ScoreText;
    /* Clear Panel */
    [SerializeField] GameObject ClearPanel;
    public bool isClearOpen = false;

    /* Main Scene �������� */
    public bool isMainSceneDone = false;  
    public static CanvasManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!instance)
            {
                instance = FindObjectOfType(typeof(CanvasManager)) as CanvasManager;

                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; // �̺�Ʈ�� �߰�


        ReplayButton1.onClick.AddListener(OnReplayButtonClicked);
        NextButton.onClick.AddListener(OnNextButtonClicked);
        ReplayButton2.onClick.AddListener(OnReplayButtonClicked);
        ExitButton.onClick.AddListener(OnExitButtonClicked);
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ���� ����*  
    }

    //void Start()
    //{
    //    if (instance != null)
    //    {
    //        DestroyImmediate(this.gameObject);
    //        return;
    //    }
    //    instance = this;

    //    DontDestroyOnLoad(gameObject);

    //    SceneManager.sceneLoaded += OnSceneLoaded; // �̺�Ʈ�� �߰�

    //}

    // Update is called once per frame
    void Update()
    {

    }
    public StageLevel GetStageLevel()
    {
        return stageLevel;
    }
    public void OpenRetryExitPanel()
    {
        RetryExitPanel.SetActive(true);
        isRetryExitOpen = true;
    }
    public GameObject GetRetryExitPanel()
    {
        return RetryExitPanel;
    }
    public void OpenClearPanel()
    {
        ClearPanel.SetActive(true);
        isClearOpen = true;
        StartCoroutine(DisableClearForSeconds());  
    }
    IEnumerator DisableClearForSeconds()
    {
        yield return new WaitForSeconds(2.0f);
        ClearPanel.SetActive(false);
    }
    IEnumerator DisableScoreForeSeconds()
    {
        yield return new WaitForSeconds(2.0f);  
        ScorePanel.SetActive(false);
    }

    public void OpenScorePanel()
    {
        ScorePanel.SetActive(true);
        string scoreTxt = string.Format("{0:n0}", PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().score);

        ScoreText.text = "Score : " + scoreTxt;

        StartCoroutine(DisableScoreForeSeconds());

    }
    public void OpenRetryOrNextPanel()
    {
        RetryNextPanel.SetActive(true);
    }
    public void ChangeScene(string sceneName)
    {
        Fade_img.gameObject.SetActive(true);
        Fade_img.alpha = 0;
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() =>
        {
            Fade_img.blocksRaycasts = true;
        })
        .OnComplete(() =>
        {
            StartCoroutine("LoadScene", sceneName); /// �� �ε� �ڷ�ƾ ���� ///  
            //StartCoroutine(LoadScene(sceneName)); /// �� �ε� �ڷ�ƾ ���� ///    
        });

    }

    IEnumerator LoadScene(string sceneName)
    {
        PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = false;

        LoadingUI.SetActive(true); //�ε� ȭ���� ���  

        /* ���ξ� �������� üũ */
        //Loading_UI loadingUIComponent = LoadingUI.GetComponent<Loading_UI>();
        //if (loadingUIComponent != null)
        //{
        //    loadingUIComponent.SetActiveLoadingMushRoom(true);
        //    loadingUIComponent.SetActiveLoadingText(true);
        //    loadingUIComponent.SetActiveProgressBar(true);  
        //}

        Loading_UI loadingUIComponent = LoadingUI.GetComponent<Loading_UI>();

        if (isMainSceneDone == true)
        {
            if (loadingUIComponent != null)
            {
                loadingUIComponent.SetActiveLoadingMushRoom(false);
                loadingUIComponent.SetActiveLoadingText(false);
                loadingUIComponent.SetActiveProgressBar(false);
                loadingUIComponent.SetActiveStartImage(true);
            }
        }
        else
        {
            loadingUIComponent.SetActiveStartImage(false);  
        }

        if (ProgressBar != null)
            ProgressBar.fillAmount = 0;

        switch (sceneName)
        {
            case "YJ":
                stageLevel = StageLevel.YJ;
                StageLevelText.text = "Stage 1";
                break;
            case "HE":
                stageLevel = StageLevel.HE;
                StageLevelText.text = "Stage 2";
                break;
            case "DH":
                stageLevel = StageLevel.DH;
                StageLevelText.text = "Stage 3";
                break;
            case "CM":
                stageLevel = StageLevel.CM;
                StageLevelText.text = "Stage 4";
                break;
            case "WG":
                stageLevel = StageLevel.WG;
                StageLevelText.text = "Stage 5";
                break;
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false; //�ۼ�Ʈ ����̿�

        float past_time = 0;
        float percentage = 0;


        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true; //�� ��ȯ �غ� �Ϸ�

                    RetryNextPanel.SetActive(false);
                    RetryExitPanel.SetActive(false);
                    ClearPanel.SetActive(false);
                    ScorePanel.SetActive(false);
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }


            Loading_text.text = percentage.ToString("0") + "%"; //�ε� �ۼ�Ʈ ǥ��
            ProgressBar.fillAmount = percentage / 100;

        }

        isMainSceneDone = true;

        //RetryNextPanel.SetActive(false);
        //RetryExitPanel.SetActive(false);
        //ClearPanel.SetActive(false);
        //ScorePanel.SetActive(false);
        //isRetryExitOpen = false;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration)
        .OnStart(() =>
        {
            LoadingUI.SetActive(false);

        })
        .OnComplete(() =>
        {
            Fade_img.blocksRaycasts = false;
            RetryNextPanel.SetActive(false);
            RetryExitPanel.SetActive(false);
            ClearPanel.SetActive(false);
            ScorePanel.SetActive(false);

            //Time.timeScale = 1;
            //PlayerControlManager.Instance.GetPlayer().transform.position = PlayerControlManager.Instance.GetStartPos().transform.position;
            //PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = false;
        });
    }

    public void OnReplayButtonClicked()
    {
        Debug.Log("Retry");

        //ChangeScene(stageLevel.ToString());
        ChangeScene("YJ");  
        //SceneManager.LoadScene(stageLevel.ToString());  

        RetryExitPanel.SetActive(false);
        RetryNextPanel.SetActive(false);


        Time.timeScale = 1;
        PlayerControlManager.Instance.GetPlayer().transform.position = PlayerControlManager.Instance.GetStartPos().transform.position;
        PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = false;
        PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().score = 0;  
    }

    //public void OnReplayButtonClicked2()
    //{
    //    Debug.Log("Retry2");
    //    ChangeScene(stageLevel.ToString());
    //    //SceneManager.LoadScene(stageLevel.ToString());  

    //    RetryExitPanel.SetActive(false);
    //    Time.timeScale = 1;
    //    PlayerControlManager.Instance.GetPlayer().transform.position = PlayerControlManager.Instance.GetStartPos().transform.position;
    //    PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = false;

    //}
    public void OnNextButtonClicked()
    {
        Debug.Log("Next");

        if ((int)stageLevel == 5 || (int)stageLevel == 0)
            return;

        RetryNextPanel.SetActive(false);
        int nextStageLevel = ((int)stageLevel) + 1;
        ChangeScene(((StageLevel)nextStageLevel).ToString());

        //PlayerControlManager.Instance.GetPlayer().transform.position = PlayerControlManager.Instance.GetStartPos().transform.position;
        //PlayerControlManager.Instance.GetPlayer().GetComponent<HE_Player>().StageClear = false;  
    }
    public void OnExitButtonClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
