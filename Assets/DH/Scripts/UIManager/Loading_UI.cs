using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_UI : MonoBehaviour
{
    [SerializeField] GameObject LoadingText; 
    [SerializeField] GameObject LoadingMushRoom; 
    [SerializeField] GameObject ProgressBar;
    [SerializeField] GameObject StartImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetActiveLoadingText(bool flag)
    {
        LoadingText.gameObject.SetActive(flag);
    }
    public void SetActiveLoadingMushRoom(bool flag)
    {
        LoadingMushRoom.gameObject.SetActive(flag);

    }
    public void SetActiveProgressBar(bool flag)
    {
        ProgressBar.gameObject.SetActive(flag);

    }
    public void SetActiveStartImage(bool flag)
    {
        StartImage.gameObject.SetActive(flag);
    }
}
