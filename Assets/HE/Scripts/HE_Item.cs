using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE_Item: MonoBehaviour
{
    public float Speed = 4.0f;
    Rigidbody2D rbody = null;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.AddForce(new Vector3(Speed, Speed, 0));
    }

    void Update()
    {
        Destroy(gameObject, 3.0f);
    }
}
