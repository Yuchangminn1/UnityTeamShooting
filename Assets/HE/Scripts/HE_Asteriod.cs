using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE_Asteriod : MonoBehaviour
{
    public float speed = 3f;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PBullet"))
        {
            anim.SetBool("break", true);
            Destroy(gameObject, 1f);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
