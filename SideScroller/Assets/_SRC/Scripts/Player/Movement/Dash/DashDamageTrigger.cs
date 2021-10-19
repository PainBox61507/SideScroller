using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class DashDamageTrigger : MonoBehaviour
    {
        [HideInInspector]public PlayerDash playerDash;
        [SerializeField] AttackType attackType;

        private void Start()
        {
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyController enemyController = other.GetComponent<EnemyController>();
                enemyController.RecieveDamage(attackType);
            }
        }
    }
}