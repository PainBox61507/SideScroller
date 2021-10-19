using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class KillPlayerOnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
                {
                    playerController.RecieveDamage();
                }
            }
        }
    }
}