using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    private Collider2D itemCollider2D;
    public virtual void Awake()
    {
        itemCollider2D = GetComponent<Collider2D>();
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball")) {
            CollisionWithBall();
        }
    }


    public virtual void CollisionWithBall() { 
        
    }


    public void SetCollier_Enable(bool _isEnable) {
        itemCollider2D.enabled = _isEnable;
    }
}
