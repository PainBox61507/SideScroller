using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class TimeSwitch : EnemyMonoStateManager
    {
        [SerializeField] private EnemyState firstState;
        [SerializeField] private EnemyState secondState;

        [SerializeField] private float timeBetweenStates = 1;
        private float timePassed = 0;
        bool switchState = false;

        // Start is called before the first frame update
        void Start()
        {
            ChangeState(firstState);
        }

        protected override void Update()
        {
            base.Update();
            if (timePassed >= timeBetweenStates)
            {
                timePassed = 0;
                if (switchState)
                {
                    ChangeState(firstState);
                    switchState = false;
                    //print(firstState.GetType().Name);
                }
                else
                {
                   ChangeState(secondState);
                    switchState = true;
                }
            }
            timePassed += Time.deltaTime;
        }
        protected override void CurrentStateFinished()
        {
            timePassed += 9999;
        }
    }
}