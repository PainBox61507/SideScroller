using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class ByTransformLateralMovementState : EnemyState
    {
        Rigidbody rb;
        [SerializeField] float speed = 4;
        Vector2 direction;

        private Transform animatorTransform;

        // Start is called before the first frame update
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            direction = this.transform.right;
        }

        protected override void Start()
        {
            base.Start();
            animatorTransform = GetComponentInChildren<Animator>(true).transform;

        }

        //void OnTriggerEnter(Collider collision)
        //{
        //    if (collision.CompareTag("Flag"))
        //    {
        //        // this.transform.eulerAngles += new Vector3(0, 180, 0);
        //        direction *= -1;
        //    }
        //}

        // Update is called once per frame
        public override void FixedUpdateState()
        {
            float move = direction.x * speed;
            rb.velocity = new Vector3(move, rb.velocity.y, rb.velocity.z);
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
            if (GetAngle(Vector3.right) < 90)
            {
                direction = Vector3.right;
            }
            if (GetAngle(Vector3.left) < 90)
            {
                direction = Vector3.left;
            }
        }

        private float GetAngle(Vector3 _vectorDir)
        {
            return Vector3.Angle(_vectorDir, animatorTransform.right);
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