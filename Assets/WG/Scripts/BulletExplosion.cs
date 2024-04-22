using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    [SerializeField][Range(0f,1f)] float LifeTime = 0.4f;
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }
}
