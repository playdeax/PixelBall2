using System;
using System.Collections;
using UnityEngine;

namespace BlueBall.Scripts.Boss
{
    public class BossBase:EnemyBase
    {
        [SerializeField] protected int healthPoint;
        [SerializeField] private EnemyBaseBeHit beHit;
        [SerializeField] private SpriteRenderer spriteEnemy;
        [SerializeField] private ParticleSystem efxBeHit;
        [SerializeField] private GameObject stunObj;
        protected bool isAction;
        private void Start()
        {
            BossHPGroup.Instance.gameObject.SetActive(true);
            BossHPGroup.Instance.Init(healthPoint);
            beHit.SetCollier_Enable(false);
            SetCollier_Enable(true);
            StartCoroutine(CallFunctionDelay(3f, Action));
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
            BossHPGroup.Instance.DecreaseHP(1);
            OnBossHurt();
            StartCoroutine(SetEnemyBeHit_IEnumerator());
            GameLevelManager.Ins.SetBall_JumpOnEnemy(_forceAddBall2);
               
        }

        private IEnumerator SetEnemyBeHit_IEnumerator()
        {
            efxBeHit.gameObject.SetActive(true);
            efxBeHit.Play();
            
            yield return new WaitForSeconds(2f);
            efxBeHit.gameObject.SetActive(false);
        }

        protected IEnumerator CallFunctionDelay(float delay,Action callback,bool cancelCondition = false)
        {
            var countTime = 0f;
            yield return null;
            while (Config.currGameState == Config.GAMESTATE.PLAYING&& countTime<delay)
            {
                countTime += Time.deltaTime;
                if(cancelCondition) yield break;
                yield return null;
            }
            if(cancelCondition) yield break;
            callback?.Invoke();
        }
        public virtual void Stun()
        {
            beHit.SetCollier_Enable(true);
            SetCollier_Enable(false);
            isAction = false;
            stunObj.SetActive(true);
        }

        public virtual void Action()
        {
            isAction = true;
            SetCollier_Enable(true);
            stunObj.SetActive(false);
            beHit.SetCollier_Enable(false);
        }

        public virtual void OnBossHurt()
        {
            beHit.SetCollier_Enable(false);
        }

        public virtual void OnBossDie()
        {
            stunObj.SetActive(false);
            SetCollier_Enable(false);
            beHit.SetCollier_Enable(false);
        }

        public void SetBossCanHit(bool isHit)
        {
            beHit.SetCollier_Enable(isHit);
        }
    }
}