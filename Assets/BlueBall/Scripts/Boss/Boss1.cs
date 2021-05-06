using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;
using UnityEngine;

namespace BlueBall.Scripts.Boss
{
    public class Boss1 : BossBase
    {
        [SerializeField] private Animator animator;
        [SerializeField] private int attackCountToStun = 2;
        [SerializeField] private float attackDelay = 1.5f;
        [SerializeField] private float stunDuration = 2f;

        [SerializeField, Sirenix.OdinInspector.MinMaxSlider(1, 10, true)]
        private Vector2 jumpDistance;

        [SerializeField, Sirenix.OdinInspector.MinMaxSlider(-100, 100, true)]
        private Vector2 moveRange;

        [SerializeField] private GameObject jumpEfx;

        private int countAttack = 0;

        public override void Action()
        {
            if (healthPoint <= 0) return;
            jumpEfx.SetActive(false);
            isHurt = false;
            base.Action();
            animator.SetTrigger("Attack");
        }

        public void StartJump()
        {
            var player = BallManager.Ins.gameObject;
            var jumpLength = Random.Range(jumpDistance.x, jumpDistance.y);
            if (player.transform.position.x < transform.position.x) // move left
            {
                var endPos = transform.position.x - jumpLength;
                if (endPos < moveRange.x)
                {
                    endPos = moveRange.x;
                }

                transform.DOMoveX(endPos, 0.8f).SetEase(Ease.OutQuint);
            }
            else // move right
            {
                var endPos = transform.position.x + jumpLength;
                if (endPos > moveRange.y)
                {
                    endPos = moveRange.y;
                }

                transform.DOMoveX(endPos, 0.8f).SetEase(Ease.OutQuint);
            }
        }

        public void OnStopJump()
        {
            ProCamera2DShake.Instance.ConstantShake("EarthquakeHard");
            Invoke(nameof(StopShake),0.5f);
            jumpEfx.SetActive(true);
        }

        void StopShake()
        {
            ProCamera2DShake.Instance.StopConstantShaking(0f);
        }
        public void OnAttackFinish()
        {
            countAttack++;
            if (countAttack % attackCountToStun == 0)
                Stun();
            else

                StartCoroutine(CallFunctionDelay(attackDelay, Action));
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
            isHurt = true;
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

        public void OnHurtEnd()
        {
           
        }

        public void OnBossDiedEnd()
        {
            BossHPGroup.Instance.OnBossDie();
            BallManager.Ins.magnetCollider.SetActive(true);
        }
    }
}