using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class DoorOpenOnKill : MonoBehaviour
    {

        [SerializeField] List<EnemyController> listOfEnemies;
        // Start is called before the first frame update
        void Start()
        {
            if (listOfEnemies.Contains(null) || listOfEnemies.Count <= 0) 
            { 
                listOfEnemies = new List<EnemyController>(FindObjectsOfType<EnemyController>());
                Debug.LogWarning($"{typeof(DoorOpenOnKill).Name} script doesnt has asigned objetives. Automaticly set to all objetives");
            }

            if (listOfEnemies.Count > 0)
            {
                foreach (var _enemy in listOfEnemies)
                {
                    _enemy.OnDeath += CheckIfAllDead;
                }

            }
            else
            {
                Debug.LogError($"No enemies are set to {this.name} on scrpit {typeof(DoorOpenOnKill).Name}");
            }
        }

        void CheckIfAllDead()
        {
            if(listOfEnemies.Count > 0)
            {
                foreach (var _enemy in listOfEnemies)
                {
                    if (_enemy != null && _enemy.gameObject.activeSelf)
                    {
                        return;
                    }
                }
            }
            OpenTheDoor();
        }

        void OpenTheDoor()
        {
            Destroy(this.gameObject);
        }

        private void OnDisable()
        {
            foreach (var _enemy in listOfEnemies)
            {
                if (_enemy != null)
                {
                    _enemy.OnDeath -= CheckIfAllDead;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            foreach (var _enemy in listOfEnemies)
            {
                if (_enemy != null)
                {
                    Gizmos.color = Color.magenta;  
                    Gizmos.DrawWireSphere(_enemy.transform.position, 1);
                }
            }
            Gizmos.DrawIcon(transform.position, "Selected", true, Color.green);

        }
    }
}
