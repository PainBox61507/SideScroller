using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

  //  public class PlayerRadarAI : EnemyMonoStateManager
   // {
        //float detectPlayerRadius;
        //[SerializeField] private EnemyState IdleState;
        //[SerializeField] private EnemyState PlayerDetectedState;

        ////private EnemyState currentState;

        ////private bool isStateReadyToChange;
        ////private bool isChangingState = false;

        //// Start is called before the first frame update
        //void Start()
        //{
        //    if (ChangeState(IdleState))
        //    {

        //    }
            

        //    //if(IdleState == null)
        //    //{
        //    //    Debug.LogError((this.name) + " " + (typeof(PlayerRadarAI).Name) + "IdleState cant be set to null");
        //    //    this.enabled = false;
        //    //}
        //}

        //// Update is called once per frame
        //void Update()
        //{
        // // if(!isChangingState) isStateReadyToChange = (bool)currentState?.Update();
        //}

        //private void FixedUpdate()
        //{
        //    if (isStateReadyToChange)
        //    {
        //        bool _playerDetected = Physics.CheckSphere(this.transform.position, detectPlayerRadius, LayerConstants.PLAYER);
        //        if (currentState != PlayerDetectedState && PlayerDetectedState != null && PlayerDetectedState.IsAvailable())
        //        {
        //            ChangeState(PlayerDetectedState);
        //        }
        //        else if(currentState != IdleState && IdleState.IsAvailable())
        //        {
        //            ChangeState(IdleState);
        //        }
        //    }

        //}

        //protected void ChangeState(EnemyState _state)
        //{
        //    isChangingState = true;
        //    currentState = _state;
        //    currentState.StartState();
        //    isChangingState = false;

        //}
   // }
}