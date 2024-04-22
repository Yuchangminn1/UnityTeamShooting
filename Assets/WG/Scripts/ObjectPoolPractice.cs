using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolPractice : MonoBehaviour
{

    [System.Serializable]
    private class ObjectInfo
    {
        //������Ʈ �̸�(��ųʸ� Ű������ Ȱ���)
        public string objectName;
        //������Ʈ Ǯ���� ������ ������Ʈ
        public GameObject prefab;
        //��� �̸� �����س�������
        public int PreMake_count;
    }

    public static ObjectPoolPractice Instance;
    //������Ʈ Ǯ �غ� �Ϸ� ǥ��
    public bool isReady { get; private set; }
    [SerializeField] private ObjectInfo[] objectinfos = null;

    //������ ������Ʈ���� Key�� ������ ���� ����
    string objectName;
    //������Ʈ ������ ��ųʸ�
    Dictionary<string, IObjectPool<GameObject>> objectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    //Ǯ���� ������Ʈ ���� ������ �� ��� �� ��ųʸ�
    Dictionary<string, GameObject> GoDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        IntoIt();
    }
    void IntoIt()
    {
        isReady = false;
        for (int i = 0; i < objectinfos.Length; i++)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>
                (CreatePoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
                true, objectinfos[i].PreMake_count, int.MaxValue);

            if (GoDic.ContainsKey(objectinfos[i].objectName))
            {
                Debug.LogFormat("{0} �̹� ��ϵ� ������Ʈ�Դϴ�.", objectinfos[i].objectName);
                return;
            }

            GoDic.Add(objectinfos[i].objectName, objectinfos[i].prefab);
            objectPoolDic.Add(objectinfos[i].objectName, pool);
            //�̸� ������Ʈ ���� �س���
            for (int a = 0; a < objectinfos[i].PreMake_count; a++)
            {
                objectName = objectinfos[i].objectName;
                Poolable PoolableGo = CreatePoolItem().GetComponent<Poolable>();
                PoolableGo.Pool.Release(PoolableGo.gameObject);
            }
        }
        Debug.Log("������Ʈ Ǯ�� �غ� �Ϸ�");
        isReady = true;
    }

    //����
    GameObject CreatePoolItem()
    {
        GameObject poolGo = Instantiate(GoDic[objectName]);
        poolGo.GetComponent<Poolable>().Pool = objectPoolDic[objectName];
        return poolGo;
    }
    // �뿩
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // ��ȯ
    private void OnReturnedToPool(GameObject poolGo)
    {
        if (poolGo == null) return;
        poolGo.SetActive(false);
    }

    
    // ����
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public GameObject GetGo(string GoName)
    {
        objectName = GoName;
        if (!GoDic.ContainsKey(GoName))
        {
            Debug.LogFormat("{0} ������ƮǮ�� ��ϵ��� ���� ������Ʈ�Դϴ�.", GoName);
            return null;
        }
        return objectPoolDic[GoName].Get();
    }
}
