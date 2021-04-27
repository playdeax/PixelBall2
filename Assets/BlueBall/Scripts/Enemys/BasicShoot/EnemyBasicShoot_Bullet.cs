using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyBasicShoot_Bullet : MonoBehaviour
{
    private EnemyBasicShoot enemyBasicShoot;
    public float distance = 10f;
    public float time = 1f;
    public SpriteRenderer spriteBullet;
    public ParticleSystem efxBullet;
    public ParticleSystem efxBulletExplosion;
    private Collider2D bulletCollider2D;

    public Vector2 forceAddBall;
    private Vector2 forceAddBall2;

    private void Awake()
    {
        bulletCollider2D = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitEnemyBasicShoot_Bullet(EnemyBasicShoot _enemyBasicShoot) {
        enemyBasicShoot = _enemyBasicShoot;

        if (enemyBasicShoot.transform.localScale.x > 0) {
            forceAddBall2 = new Vector2(forceAddBall.x, forceAddBall.y);
            gameObject.transform.DOLocalMoveX(-distance, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                BulletExpolsion();
            });
        }
        else {
            forceAddBall2 = new Vector2(-forceAddBall.x, forceAddBall.y);
            gameObject.transform.DOLocalMoveX(distance, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                BulletExpolsion();
            });
        }

    }


    public void BulletExpolsion() {
        StartCoroutine(BulletExpolsion_IEnumerator());
    }

    public IEnumerator BulletExpolsion_IEnumerator() {
        DOTween.Kill(gameObject.transform);
        spriteBullet.DOFade(0f, 0.2f).SetEase(Ease.OutQuart);
        efxBullet.Stop();

        efxBulletExplosion.gameObject.SetActive(true);
        efxBulletExplosion.Play();
        bulletCollider2D.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {

            BulletExpolsion();
            GameLevelManager.Ins.SetBallBeHit(forceAddBall2);
        }
    }
}
