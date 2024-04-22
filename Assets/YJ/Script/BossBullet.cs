using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 12f;
    Vector2 vec2 = Vector2.down;

    private void Start()
    {
        StartCoroutine(DestoryForSeconds());

    }

    IEnumerator DestoryForSeconds()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    void Update()
    {
        //transform.Translate(vec2 * speed * Time.deltaTime);
        transform.Translate(vec2 * speed * Time.deltaTime);
    }

    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }
    private void OnBecameInvisible()
    {
        if(gameObject != null)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            //GameObject.Find("GameManager").gameObject.GetComponent<GameManager>().UpdateLifeIcon();  

        }
    }
}
