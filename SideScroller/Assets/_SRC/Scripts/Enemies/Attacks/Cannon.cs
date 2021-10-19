using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiloZitare;

namespace MiloZitare
{
    public class Cannon : MonoBehaviour
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
    }

}