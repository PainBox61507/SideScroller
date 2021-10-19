using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
     [RequireComponent(typeof(ProjectileController))]
    public class GeneralProjectileBehaviour : MonoBehaviour
    {
        [Header("LifeSpam")]
        [SerializeField] bool hasInfiniteLifespam = false;
        [SerializeField] float timeInSeconds = 1;

        [Header("LimitOfBounces")]
        [SerializeField] bool hasInfiniteBounces = false;
        [SerializeField] int numberOfBounces = 1;
        int currentNumberOfBounces;

        ProjectileController projectileController;

        private void Start()
        {
            projectileController = this.GetComponent<ProjectileController>();
        }

        private void OnEnable()
        {
            currentNumberOfBounces = numberOfBounces;
            if (!hasInfiniteLifespam)
            {
                StartCoroutine(LifeSpam());
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!hasInfiniteBounces)
            {
                currentNumberOfBounces--;
                if (currentNumberOfBounces <= 0)
                {
                    DestroyProjectile();
                }
            }
        }

        private IEnumerator LifeSpam()
        {
            yield return Wait.Seconds(timeInSeconds);
            DestroyProjectile();
        }



        protected void DestroyProjectile()
        {
            projectileController.Die();
        }
    }
}