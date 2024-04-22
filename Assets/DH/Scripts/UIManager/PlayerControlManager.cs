using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    private static PlayerControlManager instance;
    [SerializeField] GameObject StartPos;
    [SerializeField] GameObject EndPos;

    public GameObject GetPlayer()
    {
        return this.gameObject.transform.GetChild(0).gameObject;
    }
    public void SetActivePlayer(bool flag)
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(flag);
    }
    public static PlayerControlManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!instance)
            {
                instance = FindObjectOfType(typeof(PlayerControlManager)) as PlayerControlManager;

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

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject GetStartPos()
    {
        return StartPos;
    }
    public GameObject GetEndPos()
    {
        return EndPos;
    }
}
