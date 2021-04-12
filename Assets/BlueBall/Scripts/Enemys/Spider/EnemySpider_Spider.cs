using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider_Spider : EnemyBase
{
    public EnemySpider enemySpider;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CollisionWithBall()
    {
        base.CollisionWithBall();
        GameLevelManager.Ins.SetBallBeHit(enemySpider.speedAddBall,1 );
    }

    public void SetAnimator() { 
        
    }
}
