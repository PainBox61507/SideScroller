using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class BasicPatrol : EnemyMonoStateManager
    {
        [SerializeField] EnemyState Idle;
        [SerializeField] EnemyState PlayerDetected;
        [SerializeField] EnemyState TurnAround;

        [SerializeField] float PlayerDetectionRadius;

        //[SerializeField] float sideDetectionSize = 0.1f;

        LayerMask playerLayer;
        private LayerMask groundLayer1;
        private LayerMask groundLayer2;
        //private Transform animatorTransform;

        private void Start()
        {
            // playerLayer = LayerMask.GetMask(LayerMask.LayerToName(LayerConstants.PLAYER));
            playerLayer = LayerConstants.numToLayerMask(LayerConstants.PLAYER);
            //groundLayer1 = LayerConstants.numToLayerMask(LayerConstants.NAVIGABLE);
            //groundLayer2 = LayerConstants.numToLayerMask(LayerConstants.GROUND);
            //animatorTransform = GetComponentInChildren<Animator>(true).transform;
            ChangeState(Idle);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            bool _currentReadyToChange = currentState == null || currentState.IsReadyToChange();
            //print($"currentState is ready to change: {_currentReadyToChange}");
            if (_currentReadyToChange)
            {
                bool _playerDetected = false;
                Collider[] foundColliders = Physics.OverlapSphere(this.transform.position, PlayerDetectionRadius, playerLayer);

                foreach (var collider in foundColliders)
                {
                    if (collider.CompareTag(TagConstants.PLAYER))
                    {
                        _playerDetected = true;
                        break;
                    }    
                }
                if (_playerDetected)
                {
                    if (currentState != PlayerDetected) ChangeState(PlayerDetected);
                }
                else
                {
                    if(currentState != Idle)ChangeState(Idle);
                }

                //if (Physics.CheckBox(animatorTransform.right + Vector3.right * 1, Vector3.one * sideDetectionSize * 2, Quaternion.identity, playerLayer + groundLayer1 + groundLayer2))
                //{
                //    if (currentState != TurnAround)ChangeState(TurnAround);
                //}
            }
        }

        protected override void CurrentStateFinished()
        {
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(this.transform.position, PlayerDetectionRadius);
        }
    }
}