using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("FORCE ADD BALL")]
    public Vector2 speedAddBall;
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

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            CollisionWithBall();
        }
    }

    public virtual void CollisionWithBall()
    {

    }


    public void SetCollier_Enable(bool _isEnable)
    {
        enemyCollider2D.enabled = _isEnable;
    }

    public virtual void SetEnemyBeHit(Vector2 _forceAddBall) { 
        
    }
}
