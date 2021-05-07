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
            var player = BallManager.Ins.gameObject;
            if (player.transform.position.x < transform.position.x) // move left
            {
                transform.localScale = new Vector3(1,1,1);
            }
            else // move right
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        }

        public void StartJump()
        {
            
            var player = BallManager.Ins.gameObject;
            var jumpLength = Random.Range(jumpDistance.x, jumpDistance.y);
            if (player.transform.position.x < transform.position.x) // move left
            {
                transform.localScale = new Vector3(1,1,1);
                var endPos = transform.position.x - jumpLength;
                if (endPos < moveRange.x)
                {
                    endPos = moveRange.x;
                }

                transform.DOMoveX(endPos, 1.2f).SetEase(Ease.OutQuart);
            }
            else // move right
            {
            
                transform.localScale = new Vector3(-1,1,1);
                var endPos = transform.position.x + jumpLength;
                if (endPos > moveRange.y)
                {
                    endPos = moveRange.y;
                }

                transform.DOMoveX(endPos, 1.2f).SetEase(Ease.OutQuart);
            }
        }

        public void OnStopJump()
        {
            ProCamera2DShake.Instance.ConstantShake("EarthquakeHard");
            SoundManager.instance.SFX_Boss1_Attack();
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
                Invoke(nameof(Action),attackDelay);
        }

        public override void Stun()
        {
            base.Stun();
            Invoke(nameof(Action),stunDuration);
        }

        private bool isHurt;

        public override void OnBossHurt()
        {
            base.OnBossHurt();
            isHurt = true;
            CancelInvoke();
            if (healthPoint > 0)
                Invoke(nameof(Action),0.5f);
            else
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