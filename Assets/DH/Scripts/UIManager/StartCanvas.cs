using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField] Button StartBtn;
    [SerializeField] Button ExitBtn;

    private void Awake()
    {
        if (StartBtn != null)
        {
            StartBtn.onClick.AddListener(OnStartButtonClicked);
        }
        if (ExitBtn != null)
        {
            ExitBtn.onClick.AddListener(OnExitButtonClicked);
        }
    }

    void OnStartButtonClicked()
    {
        CanvasManager.Instance.ChangeScene("YJ");
       // CanvasManager.Instance.ChangeScene("WG");
    }
    void OnExitButtonClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
