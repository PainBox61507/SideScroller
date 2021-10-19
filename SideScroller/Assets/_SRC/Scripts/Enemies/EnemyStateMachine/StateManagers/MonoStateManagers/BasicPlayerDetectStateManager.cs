using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class BasicPlayerDetectStateManager : EnemyMonoStateManager
    {
        [SerializeField] EnemyState Idle;
        [SerializeField] EnemyState PlayerDetected;

        [SerializeField] float detectionRadius = 5;
        /// <summary>
        /// Thes is the amount that the detection radius will increase once the player is detected.
        /// </summary>
        [SerializeField] float detectionRadiusIncrease;

        float currentDetectionRadius;

        //[SerializeField] float sideDetectionSize = 0.1f;

        LayerMask playerLayer;
        //private Transform animatorTransform;

        private void Start()
        {
            // playerLayer = LayerMask.GetMask(LayerMask.LayerToName(LayerConstants.PLAYER));
            playerLayer = LayerConstants.numToLayerMask(LayerConstants.PLAYER);
            //animatorTransform = GetComponentInChildren<Animator>(true).transform;
            ChangeState(Idle);
            currentDetectionRadius = detectionRadius;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            bool _currentReadyToChange = currentState == null || currentState.IsReadyToChange();
            //print($"currentState is ready to change: {_currentReadyToChange}");
            if (_currentReadyToChange)
            {
                bool _playerDetected = false;
                Collider[] foundColliders = Physics.OverlapSphere(this.transform.position, detectionRadius, playerLayer);

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
                    currentDetectionRadius = detectionRadius + detectionRadiusIncrease;
                    if (currentState != PlayerDetected) ChangeState(PlayerDetected);
                }
                else
                {
                    currentDetectionRadius = detectionRadius;
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
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, detectionRadius);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(this.transform.position, detectionRadius + detectionRadiusIncrease);
        }
    }
}