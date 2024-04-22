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
        //오브젝트 이름(딕셔너리 키값으로 활용됨)
        public string objectName;
        //오브젝트 풀에서 관리할 오브젝트
        public GameObject prefab;
        //몇개를 미리 생성해놓을건지
        public int PreMake_count;
    }

    public static ObjectPoolPractice Instance;
    //오브젝트 풀 준비 완료 표시
    public bool isReady { get; private set; }
    [SerializeField] private ObjectInfo[] objectinfos = null;

    //생성할 오브젝트들의 Key값 지정을 위한 변수
    string objectName;
    //오브젝트 관리용 딕셔너리
    Dictionary<string, IObjectPool<GameObject>> objectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    //풀에서 오브젝트 새로 생성할 때 사용 될 딕셔너리
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
                Debug.LogFormat("{0} 이미 등록된 오브젝트입니다.", objectinfos[i].objectName);
                return;
            }

            GoDic.Add(objectinfos[i].objectName, objectinfos[i].prefab);
            objectPoolDic.Add(objectinfos[i].objectName, pool);
            //미리 오브젝트 생성 해놓기
            for (int a = 0; a < objectinfos[i].PreMake_count; a++)
            {
                objectName = objectinfos[i].objectName;
                Poolable PoolableGo = CreatePoolItem().GetComponent<Poolable>();
                PoolableGo.Pool.Release(PoolableGo.gameObject);
            }
        }
        Debug.Log("오브젝트 풀링 준비 완료");
        isReady = true;
    }

    //생성
    GameObject CreatePoolItem()
    {
        GameObject poolGo = Instantiate(GoDic[objectName]);
        poolGo.GetComponent<Poolable>().Pool = objectPoolDic[objectName];
        return poolGo;
    }
    // 대여
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        if (poolGo == null) return;
        poolGo.SetActive(false);
    }

    
    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public GameObject GetGo(string GoName)
    {
        objectName = GoName;
        if (!GoDic.ContainsKey(GoName))
        {
            Debug.LogFormat("{0} 오브젝트풀에 등록되지 않은 오브젝트입니다.", GoName);
            return null;
        }
        return objectPoolDic[GoName].Get();
    }
}
