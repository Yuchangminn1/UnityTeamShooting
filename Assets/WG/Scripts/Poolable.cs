using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//�� ��ũ��Ʈ �� �ֵ鸸 ������Ʈ Ǯ�� �ְ� �����Ұ���
public class Poolable : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    public void ReleaseObject()
    {
        //Ǯ�� �־���
        Pool.Release(gameObject);
    }
}
