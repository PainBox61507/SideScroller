using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class ChangeColorOnDamaged : MonoBehaviour
    {
        EnemyController enemyController;
        Material material;

        [SerializeField] Color newColor;
        Color previousColor;
        [SerializeField] float timeToReturn = 0.1f; 

        private void Start()
        {
           enemyController = GetComponent<EnemyController>();
            enemyController.OnReceiveDamage += ChangeColor;
            material = enemyController.GetComponent<Renderer>().material;
            previousColor = material.color;
        }

        void ChangeColor()
        {
            StartCoroutine(ChangeColorCoroutine());
        }

        IEnumerator ChangeColorCoroutine()
        {
            material.color = newColor;
            yield return Wait.Seconds(timeToReturn);
            material.color = previousColor;

        }

        private void OnDisable()
        {
            if(enemyController != null)
            {
                enemyController.OnReceiveDamage -= ChangeColor;
            }
            StopAllCoroutines();
        }

    }
}
