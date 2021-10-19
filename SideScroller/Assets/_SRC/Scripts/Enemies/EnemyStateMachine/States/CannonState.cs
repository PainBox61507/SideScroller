using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiloZitare;

namespace MiloZitare
{
    public class CannonState : EnemyState
    {
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject bulletPrefab;
        [Header("FireSettings")]
        [SerializeField] float currentFireRate = 1;
        [SerializeField] float bulletForce = 4;
        [Space]
        [SerializeField] int numberOfShots = 1;
        [SerializeField] float burstFireRate;

        private void OnEnable()
        {
            StartCoroutine(ShootSecuence());
        }
        protected virtual IEnumerator ShootSecuence()
        {
            for (int i = 0; i < numberOfShots; i++)
            {
                Shoot(firePoint, bulletPrefab, bulletForce);
                //yield return new WaitForSeconds(burstFireRate);

                yield return Wait.Seconds(burstFireRate);//StartCoroutine(EstaperarSegundos(burstFireRate));
            }


            yield return Wait.Seconds(currentFireRate);
            StartCoroutine(ShootSecuence());
        }


        protected virtual void Shoot(Transform _firePoint, GameObject _projectilePrefab, float _projectileForce) //Creates the projectile in the firepoint 
        {
            GameObject bullet = ObjectCallingAndDestroying.Summon(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.right * _projectileForce, ForceMode.Impulse);

        }

        public override bool IsAvailable()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsReadyToChange()
        {
            throw new System.NotImplementedException();
        }

        public override void StartState()
        {
            throw new System.NotImplementedException();
        }

        public override void StopState()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override void FixedUpdateState()
        {
            throw new System.NotImplementedException();
        }

        protected override void InstantiateAnimatorController(Transform animatorTransform, Animator animator)
        {
            throw new System.NotImplementedException();
        }
    }

}