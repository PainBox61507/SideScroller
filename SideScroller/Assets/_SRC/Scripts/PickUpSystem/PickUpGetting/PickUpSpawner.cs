using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class PickUpSpawner : CanPickUp
    {
        [SerializeField]public float respawnCD = 0.1f;

        PickUpController spawnedPickUp;

        private void Start()
        {
            TakePickUp();
        }

        public override PickUpController TakePickUp()
        {
            if (this.transform.childCount > 0)
            {
                spawnedPickUp = this.transform.GetChild(0).gameObject.GetComponent<PickUpController>();
                spawnedPickUp.OnUseFinished += RespawnPickUp;

                return spawnedPickUp;
            }
            return null;
        }

        private void RespawnPickUp()
        {
            StartCoroutine(ReSpawnPickUp());
        }
        IEnumerator ReSpawnPickUp()
        {
            yield return Wait.Seconds(respawnCD);
            spawnedPickUp.transform.position = this.transform.position;
            spawnedPickUp.transform.parent = this.transform;
            spawnedPickUp.gameObject.SetActive(true);
        }

    }
}