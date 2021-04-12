using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : EnemyBase
{
    public bool isWater;
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
        if (isWater)
        {
            GameLevelManager.Ins.SetBallBeHit(speedAddBall, 3);
        }
        else
        {
            GameLevelManager.Ins.SetBallBeHit(speedAddBall, 1);
        }
        
    }


    public void Bite()
    {
      //  SoundManager.instance.SFX_Bite();
    }

}
