using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class SingleState : EnemyStateManager
    {
        [SerializeField] private EnemyState state;

        // Start is called before the first frame update
        void Awake()
        {
            activeStates = new EnemyState[] { state };
        }
    }
}