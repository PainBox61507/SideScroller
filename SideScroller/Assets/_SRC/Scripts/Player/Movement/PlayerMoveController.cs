using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class PlayerMoveController : MonoBehaviour
    {
        Rigidbody rb;
        [Space]
        [Header("Movement")]
        [SerializeField] float accelerationTime = 0.1f;
        public float maxSpeed = 6.5f;
        float move = 1;

        Vector3 refMovementVector = Vector3.zero;

        PlayerAnimationController animationPlayerController;

        // Start is called before the first frame update
        void Start()
        {
            //collisionManager = this.GetComponent<CollisionManager>();
            animationPlayerController = this.GetComponentInChildren<PlayerAnimationController>();
            rb = GetComponent<Rigidbody>();
            GetInput();
        }

        // Update is called once per frame
        void Update()
        {
            Controls();
        }

        void Controls()
        {
            GetInput();
        }
        void GetInput()
        {
            move = Input.GetAxisRaw("Horizontal");
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (PlayerInput.Horizontal() > 0)
            {
                this.transform.eulerAngles = Vector3.zero;
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            float _move = move * maxSpeed;
            Vector3 targetVelocity = new Vector3(_move, rb.velocity.y, rb.velocity.z);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refMovementVector, accelerationTime);
            animationPlayerController?.Walk(rb.velocity.x);
        }
    }

}
