using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class PlayerJumpController : MonoBehaviour
    {
        [Header("Jump")]
        [SerializeField] int maxExtraJumps = 0;
        [Space]
        [SerializeField] float jumpForce = 10;
        [SerializeField] float fJumpPressedRememberTime = 0.2f;
        float fJumpPressedRemember;
        //[SerializeField] float jumpAccelerationTime = 0;
        //[SerializeField] float fallingAcceleration = 15;
        //[SerializeField] float jumpReduction = 10;
        int currentExtraJumps;
        [Space]

        [Header("Jump Midair Cut")]
        [SerializeField] float fCutJumpHeight = 0.7f;
        [SerializeField] float fCutJumpTime = 0.2f;
        float fCutJumpTimeRemember;

        [Header("NEW JUMP MODS")]
        [SerializeField] float lowJumpMultiplier = 2f;
        [SerializeField] float fallMultiplier = 2.5f;

        [HideInInspector] bool isGrounded;


        //CONTROL MEMORY//
        bool ctrlJumpDown;
        bool ctrlJumpUp;

        private Rigidbody rb;
        CollisionManager collisionManager;

        PlayerAnimationController animationPlayerController;

        void Start()
        {
            collisionManager = this.GetComponent<CollisionManager>();
            rb = GetComponent<Rigidbody>();
            animationPlayerController = GetComponentInChildren<PlayerAnimationController>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                ctrlJumpDown = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                ctrlJumpUp = true;
            }
        }

        void FixedUpdate()
        {
            isGrounded = collisionManager.IsGrounded;

            if (ctrlJumpDown) //JUMP REMEMEMBER
            {
                fJumpPressedRemember = fJumpPressedRememberTime;
            }

            if (ctrlJumpUp && fCutJumpTimeRemember > 0) //CUT JUMP
            {
                if (rb.velocity.y > 0)
                {
                   rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * (1 - fCutJumpHeight));
                }
            }

            if (isGrounded) //GROUNDED VARIABLES UPDATE
            {
                currentExtraJumps = maxExtraJumps;
            }

            if (fJumpPressedRemember > 0 && collisionManager.groundedRemember > 0) //JUMP//JUMP//JUMP//
            {
                fJumpPressedRemember = 0;
                collisionManager.groundedRemember = 0;
                fCutJumpTimeRemember = fCutJumpTime;

                Jump();

            }
            else if (currentExtraJumps > 0 && ctrlJumpDown && !isGrounded) //SECOND JUMP//
            {
                fJumpPressedRemember = 0;
                currentExtraJumps -= 1;
                Jump();
            }



            fJumpPressedRemember -= Time.fixedDeltaTime;
            collisionManager.groundedRemember -= Time.fixedDeltaTime;
            fCutJumpTimeRemember -= Time.fixedDeltaTime;
            ctrlJumpDown = false;
            ctrlJumpUp = false;
        }

        private void Jump()
        {
            animationPlayerController?.Jump();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //animationPlayerController.Jump();
        }

        private void GravityMods()
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0 && ctrlJumpUp && fCutJumpTimeRemember > 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
    }
}
