using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyBasicShoot : EnemyBase2
{
    public Animator animator;
    public float timeWaitBullet;

    public EnemyBasicShoot_Bullet bulletPrefab;
    public ParticleSystem efxBulletMuzzle;
    public Transform posStartBullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitBasicShoot_IEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SetEnemyBeHit(Vector2 _forceAddBall2)
    {
        base.SetEnemyBeHit(_forceAddBall2);
        isDead = true;
    }
    bool isDead = false;
    public IEnumerator InitBasicShoot_IEnumerator() {
        while (!isDead)
        {
            yield return new WaitForSeconds(timeWaitBullet);
            animator.SetTrigger("shoot");
            yield return new WaitForSeconds(0.02f);
            EnemyBasicShoot_Bullet bullet = Instantiate<EnemyBasicShoot_Bullet>(bulletPrefab, GameLevelManager.Ins.efxTranform);
            bullet.InitEnemyBasicShoot_Bullet(this);
            bullet.transform.position = posStartBullet.position;
        }
    }
}
