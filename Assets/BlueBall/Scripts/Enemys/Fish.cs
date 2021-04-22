using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : EnemyBase
{
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
        GameLevelManager.Ins.SetBallBeHit(speedAddBall);
    }


    public void Bite()
    {
      //  SoundManager.instance.SFX_Bite();
    }

}
