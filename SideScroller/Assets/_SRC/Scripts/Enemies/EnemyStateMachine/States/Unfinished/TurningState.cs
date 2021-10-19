using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class TurningState : EnemyState
    {
        [SerializeField]float turnSpeed = 180f;

        Rigidbody rb;
        Transform animatorTransform;

        public bool IsTurning { get; private set; }

        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
            animatorTransform = GetComponentInChildren<Animator>(true).transform;
            StartCoroutine(TurnSequence(Vector3.left));

        }


        public override bool IsAvailable()
        {
            return true;
        }

        public override bool IsReadyToChange()
        {
            return !IsTurning;
        }

        public override void StartState()
        {
            rb.velocity = Vector3.zero;
            if (GetAngle(Vector3.right) < 90)
            {
                //print("to right is " + GetAngle(Vector3.right));

                StartCoroutine(TurnSequence(Vector3.left));
            }
            if (GetAngle(Vector3.left) < 90)
            {
                //print("to left is " + GetAngle(Vector3.left));

                StartCoroutine(TurnSequence(Vector3.right));
            }
        }

        private IEnumerator TurnSequence(Vector3 _desiredDirection)
        {
            IsTurning = true;
            while (GetAngle(_desiredDirection) != 0)
            {
                float _turnAmount = turnSpeed * Time.deltaTime;
                if (GetAngle(_desiredDirection) - _turnAmount <= 0)
                {
                    animatorTransform.Rotate(Vector3.up * GetAngle(_desiredDirection));
                }
                else
                {
                    animatorTransform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                }
                yield return null;
            }
            IsTurning = false;
            StateFinished();
        }

        public override void StopState()
        {
            IsTurning = false;//throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            
        }

        private float GetAngle(Vector3 _vectorDir)
        {
            return Vector3.Angle(_vectorDir, animatorTransform.right);
        }

        protected override void InstantiateAnimatorController(Transform animatorTransform, Animator animator)
        {
           // throw new System.NotImplementedException();
        }

        public override void FixedUpdateState()
        {
            // throw new System.NotImplementedException();
        }
    }
}