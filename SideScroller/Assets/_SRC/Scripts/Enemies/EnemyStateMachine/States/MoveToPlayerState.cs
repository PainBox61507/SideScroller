using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class MoveToPlayerState : EnemyState
    {
        [SerializeField]float followStrength = 5f;

        Transform playerTransform;
        Rigidbody rb;

        protected override void Start()
        {
            base.Start();
            playerTransform = FindObjectOfType<PlayerController>(true).transform;
            rb = GetComponent<Rigidbody>();
        }
        public override void FixedUpdateState()
        {
            Vector3 vectorToPlayer = (playerTransform.position - this.transform.position).normalized * followStrength;
            rb.velocity = vectorToPlayer;
        }

        public override bool IsAvailable()
        {
            return true;
        }

        public override bool IsReadyToChange()
        {
            return true;
        }

        public override void StartState()
        {
        }

        public override void StopState()
        {
        }

        public override void UpdateState()
        {
        }

        protected override void InstantiateAnimatorController(Transform animatorTransform, Animator animator)
        {
        }
    }
}