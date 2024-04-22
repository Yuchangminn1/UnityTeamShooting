using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    [SerializeField] Button loadingButton;
    // Start is called before the first frame update
    private void Awake()
    {
        loadingButton.onClick.AddListener(OnLoadingButtonClicked);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLoadingButtonClicked()
    {
        Debug.Log("OnLoadingButtonClicked");
        CanvasManager.Instance.ChangeScene("YJ");    
    }
}
