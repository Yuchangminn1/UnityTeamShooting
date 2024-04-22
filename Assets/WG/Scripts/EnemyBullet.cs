using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Pool;
//Poolable ����ؼ� Poolable �� �ִ� Pool������Ƽ ����� Poolable�پ��ִ� ������Ʈ�� ����
public class EnemyBullet : Poolable
{
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float BulletLerpDuration = 1f;
    Vector2 vec2;
    public float Speed { get { return MoveSpeed; } set { MoveSpeed = value; } }
    public float Lerp_Duration { get { return BulletLerpDuration; } set { BulletLerpDuration = value; } }
    //�ߺ� Release������
    public bool isReturnedToPool = false;
    private void Start()
    {
    }
    void Update()
    {
        transform.Translate(vec2 * MoveSpeed * Time.deltaTime);
    }
    //������ ���� ���� ����
    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player_HitBox"))
        {
            gameObject.SetActive(false);
        }
    }
    // Ǯ���� ������ �� ȣ��Ǵ� �̺�Ʈ
    private void OnEnable()
    {
        isReturnedToPool = false; // ��ȯ ���� �ʱ�ȭ
    }
    private void OnBecameInvisible()
    {
        DuplicateCheck();
    }

    private void DuplicateCheck()
    {
        if (isReturnedToPool)
        {
            return;
        }

        // Ǯ�� ��ȯ
        Pool.Release(gameObject);
        isReturnedToPool = true; // ��ȯ ���·� �÷��� ����
    }

    void Deactivated()
    {
        gameObject.SetActive(false);
    }
}
