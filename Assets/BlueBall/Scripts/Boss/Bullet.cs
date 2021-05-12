using DG.Tweening;
using UnityEngine;

namespace BlueBall.Scripts.Boss
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        [SerializeField] private Vector2 forceAddBall;
        private Vector3 moveDirection;


        private bool isInit;

        public void Init(Vector3 direction)
        {
            moveDirection = direction;
            isInit = true;
            transform.localEulerAngles = moveDirection;
            var endPosition = new Vector3(transform.right.x * 100f,transform.right.y * 100f,transform.right.z * 100f);
            float moveTime = Vector3.Distance(endPosition, transform.position)/moveSpeed;
            transform.DOMove(endPosition, moveTime);
            if (transform.right.x < 0)
            {
                forceAddBall.x *= -1;
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ball"))
            {
                GameLevelManager.Ins.SetBallBeHit(forceAddBall);
                Destroy(gameObject);
            }
        }
    }
}