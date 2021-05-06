using UnityEngine;

namespace BlueBall.Scripts.Boss
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;

        public void Shoot()
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.Init(transform.localEulerAngles);
        }
    }
}