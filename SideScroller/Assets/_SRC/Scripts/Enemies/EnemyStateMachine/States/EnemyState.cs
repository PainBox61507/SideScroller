using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public abstract class EnemyState : MonoBehaviour
    {
        

        /// <summary>
        /// It Runs when the state has finished its job
        /// </summary>
        public delegate void DelegateHasFinihed();
        public event DelegateHasFinihed HasFinished;
        protected void StateFinished()
        {
            HasFinished?.Invoke();
            HasFinished = null;
        }
        protected void StateFinished(EnemyState _chainState)
        {
            if(_chainState != null)
            {
               _chainState.HasFinished += this.HasFinished;
            }
            else
            {
               HasFinished?.Invoke();
            }
        }


        /// <summary>
        /// It returns true when this State can be set On
        /// </summary>
        /// <returns></returns>
        public abstract bool IsAvailable();

        /// <summary>
        /// It Returns true when this State is ready to set Off
        /// </summary>
        /// <returns></returns>
        public abstract bool IsReadyToChange();

        public abstract void StartState();

        public abstract void StopState();

        public abstract void UpdateState();

        public abstract void FixedUpdateState();

        private void OnDisable()
        {
            StopAllCoroutines();
            
        }

        protected virtual void Start()
        {
           Animator _animator = GetComponentInChildren<Animator>();
            if(_animator != null)
            {
                InstantiateAnimatorController(_animator.transform, _animator);
            }
        }

        protected abstract void InstantiateAnimatorController(Transform animatorTransform, Animator animator);

        //protected void Start()
        //{
        //    EnemyStateStart();
        //   // StopState();
        //}


        //{
        //    this.enabled = false;
        //}

        /// <summary>
        /// Returns true when the state has finished its job
        /// </summary>
        /// <returns></returns>
        //public abstract bool Update();


    }
}
