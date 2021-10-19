using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiloZitare
{
    [RequireComponent(typeof(ProjectileController))]
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField]private AttackType attackType;
        ProjectileController projectileController;

        private void Start()
        {
            projectileController = this.GetComponent<ProjectileController>();
        }


        private void OnTriggerEnter(Collider _collisionCollider)
        {
            if (_collisionCollider.gameObject.CompareTag("Enemy"))
            {
                EnemyController enemyController = _collisionCollider.transform.GetComponent<EnemyController>();
                enemyController.RecieveDamage(attackType);
                projectileController.Die();
            }
        }
    }

}
