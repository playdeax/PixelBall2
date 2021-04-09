using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemSpike : ItemBase
{
    public SpriteRenderer icon;
    public Vector2 speedAddBall;
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

        GameLevelManager.Ins.SetBallSpike(this);
    }

}
