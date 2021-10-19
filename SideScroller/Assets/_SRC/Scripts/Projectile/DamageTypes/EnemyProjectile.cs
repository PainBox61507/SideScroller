using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class EnemyProjectile : MonoBehaviour
    {
        ProjectileController projectileController;

        private void Start()
        {
            projectileController = this.GetComponent<ProjectileController>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
                {
                    playerController.RecieveDamage();
                }
            }
        }
    }
}
