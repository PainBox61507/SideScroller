using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class BasicLateralMovementState : EnemyState
    {
        Rigidbody rb;
        [SerializeField] float speed = 4;
        [SerializeField] Vector2 direction = new Vector2(-1, 0);

        // Start is called before the first frame update
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            direction = this.transform.right;
        }


        void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Flag"))
            {
                // this.transform.eulerAngles += new Vector3(0, 180, 0);
                direction *= -1;
            }
        }

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