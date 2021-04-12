using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyBasic : EnemyBase
{
    public SpriteRenderer spriteEnemy;
    public ParticleSystem efxBeHit;
    
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
        GameLevelManager.Ins.SetBallBeHit(speedAddBall, 1);
    }


    public override void SetEnemyBeHit(Vector2 _forceAddBall2)
    {
        base.SetEnemyBeHit(_forceAddBall2);
        StartCoroutine(SetEnemyBeHit_IEnumerator());
        GameLevelManager.Ins.SetBall_KillEnemy(_forceAddBall2);
    }

    public IEnumerator SetEnemyBeHit_IEnumerator() {
        SetCollier_Enable(false);
        spriteEnemy.DOFade(0f, 0.2f).SetEase(Ease.OutQuart);
        efxBeHit.gameObject.SetActive(true);
        efxBeHit.Play();

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }



}
