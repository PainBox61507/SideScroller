using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class EnemyStateManager : MonoBehaviour
    {

         protected EnemyState[] activeStates = new EnemyState[] {null};

        // Start is called before the first frame update
        protected virtual void Update()
        {
            foreach (var enemyState in activeStates)
            {
                    enemyState?.UpdateState();
            }
        }

        // Update is called once per frame
        protected virtual void FixedUpdate()
        {
            foreach (var enemyState in activeStates)
            {
                    enemyState?.FixedUpdateState();
            }
        }
    }
}