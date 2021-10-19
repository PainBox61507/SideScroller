using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class PlayerVolumeTrigger : MonoBehaviour
    {
        public PlayerDash playerDash;
       
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Enemy") || other.CompareTag("Navigable"))
            {
             //   playerDash.isGoingThrougObjects++;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Navigable"))
            {
              //  playerDash.isGoingThrougObjects--;
            }
        }
    }
}