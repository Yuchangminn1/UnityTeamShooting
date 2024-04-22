using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Pool;
//Poolable 상속해서 Poolable 에 있는 Pool프로퍼티 사용해 Poolable붙어있는 오브젝트만 관리
public class EnemyBullet : Poolable
{
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float BulletLerpDuration = 1f;
    Vector2 vec2;
    public float Speed { get { return MoveSpeed; } set { MoveSpeed = value; } }
    public float Lerp_Duration { get { return BulletLerpDuration; } set { BulletLerpDuration = value; } }
    //중복 Release방지용
    public bool isReturnedToPool = false;
    private void Start()
    {
    }
    void Update()
    {
        transform.Translate(vec2 * MoveSpeed * Time.deltaTime);
    }
    //움직임 방향 변경 가능
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
    // 풀에서 꺼내질 때 호출되는 이벤트
    private void OnEnable()
    {
        isReturnedToPool = false; // 반환 상태 초기화
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

        // 풀에 반환
        Pool.Release(gameObject);
        isReturnedToPool = true; // 반환 상태로 플래그 설정
    }

    void Deactivated()
    {
        gameObject.SetActive(false);
    }
}
