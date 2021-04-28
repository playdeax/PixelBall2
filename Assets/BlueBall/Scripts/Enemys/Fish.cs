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
    public int damage = 3;
    public override void CollisionWithBall()
    {
        base.CollisionWithBall();
        GameLevelManager.Ins.SetBallFall(damage, speedAddBall);
    }


    public void Bite()
    {
      //  SoundManager.instance.SFX_Bite();
    }

}
