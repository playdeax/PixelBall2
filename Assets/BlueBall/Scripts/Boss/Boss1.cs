using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BlueBall.Scripts.Boss
{
    public class Boss1 : EnemyBase
    {
        [SerializeField] private EnemyBaseBeHit beHit;
        [SerializeField] private float jumpDelay = 1.5f;
        [SerializeField,MinMaxSlider(1, 10, true)] private Vector2 jumpDistance;


        private void Start()
        {
            
        }

        public override void CollisionWithBall()
        {
            base.CollisionWithBall();
            GameLevelManager.Ins.SetBallBeHit(speedAddBall);
        }
    }
}