using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpring : ItemBase
{
    public Animator animatorSpring;
    public Vector2 speedAddBall = new Vector2(0f, 40f);
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
        animatorSpring.SetTrigger("jump");

        GameLevelManager.Ins.SetBallSpring(this);
        SoundManager.instance.SFX_Loxo();
    }
}
