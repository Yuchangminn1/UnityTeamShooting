using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//이 스크립트 단 애들만 오브젝트 풀에 넣고 관리할거임
public class Poolable : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    public void ReleaseObject()
    {
        //풀에 넣어줌
        Pool.Release(gameObject);
    }
}
