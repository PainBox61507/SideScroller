using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class FullRBStopState : EnemyState
    {
        private Rigidbody rb;

        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
        }

        public override void FixedUpdateState()
        {
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
            rb.velocity = Vector3.zero;
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
