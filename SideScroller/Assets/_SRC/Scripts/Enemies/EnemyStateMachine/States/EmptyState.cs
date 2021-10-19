using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class EmptyState : EnemyState
    {
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
