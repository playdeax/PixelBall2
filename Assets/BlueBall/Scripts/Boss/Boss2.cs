using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;
using UnityEngine;

namespace BlueBall.Scripts.Boss
{
    public class Boss2:BossBase
    {
        [SerializeField] private Animator animator;
        [SerializeField] private int attackCountToStun = 3;
        [SerializeField] private float attackDelay = 1f;
        [SerializeField] private float stunDuration = 2f;
        [SerializeField, Sirenix.OdinInspector.MinMaxSlider(-100, 100, true)]
        private Vector2 moveRange;
        private int countAttack = 0;
        [SerializeField] private List<Gun> guns;
        
        public override void Action()
        {
            if (healthPoint <= 0) return;
            isHurt = false;
            base.Action();
            animator.SetBool("Move",true);
            StartMove();
        }

        public void StartMove()
        {
            if (transform.position.x - moveRange.x > moveRange.y - transform.position.x) // dang o ben phai
            {
                transform.DOMoveX(moveRange.x, 3f).SetEase(Ease.Linear).OnComplete(StopMove);
            }
            else
            {
                transform.DOMoveX(moveRange.y, 3f).SetEase(Ease.Linear).OnComplete(StopMove);
            }
            ProCamera2DShake.Instance.ConstantShake("EarthquakeSoft");
        }
        void StopMove()
        {
            animator.SetBool("Move",false);
            ProCamera2DShake.Instance.StopConstantShaking(0f);
            CheckAttack();
        }

        void CheckAttack()
        {
            if (countAttack <attackCountToStun)
            {
                animator.SetTrigger("Attack");
            }
        }
        public void OnAttackFinish()
        {
            Shoot();
            countAttack++;
            if (countAttack == attackCountToStun)
            {
                Stun();
                countAttack = 0;
            }
            else
                StartCoroutine(CallFunctionDelay(attackDelay, CheckAttack));
        }

        public override void Stun()
        {
            base.Stun();
            StartCoroutine(CallFunctionDelay(stunDuration, Action, isHurt));
        }

        private bool isHurt;

        public override void OnBossHurt()
        {
            base.OnBossHurt();
            if (BallManager.Ins.gameObject.transform.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y,
                    transform.localScale.z);
            }

            if (healthPoint > 0)
                StartCoroutine(CallFunctionDelay(0.2f, Action));
            if (healthPoint <= 0)
            {
                animator.SetTrigger("Die");

                OnBossDie();
            }
        }
        public void OnBossDiedEnd()
        {
            BossHPGroup.Instance.OnBossDie();
            BallManager.Ins.magnetCollider.SetActive(true);
        }

        public void Shoot()
        {
            for (var i = 0; i < guns.Count; i++)
            {
                guns[i].Shoot();
            }
        }
    }
}