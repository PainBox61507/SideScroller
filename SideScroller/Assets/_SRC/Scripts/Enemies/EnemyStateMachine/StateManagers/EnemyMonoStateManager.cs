using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiloZitare
{
    /// <summary>
    /// Use ChangeState(); to set the running state. 
    /// </summary>
    public abstract class EnemyMonoStateManager : EnemyStateManager
    {
        protected EnemyState currentState;
        private void Awake()
        {
            activeStates = new EnemyState[1] {null};
        }
        // protected EnemyState activeStates;

        protected virtual StateReturnInfo ChangeState(EnemyState _newState)
        {
            StateReturnInfo stateReturnInfo = new StateReturnInfo(true);

           // print(stateReturnInfo.isCurrentStateReadyToChange);

            if (activeStates[0] != null)
            {
                stateReturnInfo.isCurrentStateReadyToChange = activeStates[0].IsReadyToChange();
            }


            if (_newState != null)
            {
                stateReturnInfo.isNewStateAvailable = _newState.IsAvailable();
                if (stateReturnInfo.isCurrentStateReadyToChange && stateReturnInfo.isNewStateAvailable)
                {
                    activeStates[0]?.StopState();
                    currentState = activeStates[0] = _newState;
                    _newState.HasFinished += CurrentStateFinished;
                    activeStates[0].StartState();

                    stateReturnInfo.didChange = true;
                }
            }

                  //  print($"did change:{stateReturnInfo.didChange} ReadytoChange:{stateReturnInfo.isCurrentStateReadyToChange} NextIsAvailable:{stateReturnInfo.isNewStateAvailable}");
            return stateReturnInfo;

        }

        protected abstract void CurrentStateFinished();

        public struct StateReturnInfo
        {
            public bool didChange;
            public bool isCurrentStateReadyToChange;
            public bool isNewStateAvailable;

            public StateReturnInfo(bool initiate = true)
            {
                didChange = false;
                isCurrentStateReadyToChange = true;
                isNewStateAvailable = false;
            }
        }

    }
}