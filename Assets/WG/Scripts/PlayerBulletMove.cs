using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerBulletMove : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float MoveSpeed = 20f;
    [SerializeField] GameObject Effect;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = Vector3.up * MoveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Meteor"))
        {
            Destroy(gameObject);

            
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Instantiate(Effect,transform.position,Quaternion.identity);
    }
}
