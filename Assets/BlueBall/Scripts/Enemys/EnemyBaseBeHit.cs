using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseBeHit : MonoBehaviour
{
    [Header("FORCE ADD BALL")]
    public Vector2 _forceAddBall;

    public EnemyBase enemyBase;
    private Collider2D enemyCollider2D;
    public virtual void Awake()
    {
        enemyCollider2D = GetComponent<Collider2D>();
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            CollisionWithBall();
        }
    }


    public virtual void CollisionWithBall()
    {
        enemyBase.SetEnemyBeHit(_forceAddBall);
        SetCollier_Enable(false);
    }


    public void SetCollier_Enable(bool _isEnable)
    {
        enemyCollider2D.enabled = _isEnable;
    }
}
