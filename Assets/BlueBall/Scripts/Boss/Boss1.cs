using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BlueBall.Scripts.Boss
{
    public class Boss1 : EnemyBase
    {
        [SerializeField] private int healthPoint;
        [SerializeField] private float jumpDelay = 1.5f;
        [SerializeField,MinMaxSlider(1, 10, true)] private Vector2 jumpDistance;
        [SerializeField] private EnemyBaseBeHit beHit;
        [SerializeField] private SpriteRenderer spriteEnemy;
        [SerializeField] private ParticleSystem efxBeHit;
        
        private void Start()
        {
            beHit.gameObject.SetActive(false);
        }

        public override void CollisionWithBall()
        {
            base.CollisionWithBall();
            GameLevelManager.Ins.SetBallBeHit(speedAddBall);
        }
        
        public override void SetEnemyBeHit(Vector2 _forceAddBall2)
        {
            base.SetEnemyBeHit(_forceAddBall2);
            healthPoint--;
            if(healthPoint>0)
                StartCoroutine(SetEnemyBeHit_IEnumerator());
            else
                GameLevelManager.Ins.SetBall_KillEnemy(_forceAddBall2);
        }

        public IEnumerator SetEnemyBeHit_IEnumerator()
        {
            SetCollier_Enable(false);
            //spriteEnemy.DOFade(0f, 0.2f).SetEase(Ease.OutQuart);

            efxBeHit.gameObject.SetActive(true);
            efxBeHit.Play();
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }

        private void SetBossStun()
        {
            SetCollier_Enable(false);
            
        }
    }
}