using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Scripting.APIUpdating;

namespace MiloZitare.NoUse
{

    public class PlayerMovementController : MonoBehaviour
    {
#if UNITY_EDITOR
        [Range(-1, 100)] public int targetFrameRate;
#endif

        Rigidbody rb;
        [Space]
        [Header("Movement")]
        [SerializeField] float accelerationTime;
        [SerializeField] float maxSpeed;
        float move = 1;
        //[Range(0, 1)] [SerializeField] float fHorizontalDamping = 0.4f;

        Vector3 refMovementVector = Vector3.zero;

        [Header("Jump")]
        [SerializeField] int maxExtraJumps;
        [Space]
        [SerializeField] float jumpForce;
        [SerializeField] float jumpAccelerationTime;
        [SerializeField] float fallingAcceleration;
        [SerializeField] float jumpReduction;
        int currentExtraJumps;
        [Space]

        [Header("Jump Control Tweaks")]
        [SerializeField] float fJumpPressedRememberTime;
        float fJumpPressedRemember;
        [SerializeField] float fGroundedRememberTime;
        float fGroundedRemember;
        [SerializeField] float fCutJumpTime;
        float fCutJumpTimeRemember;
        [SerializeField] float fCutJumpHeight;

        [HideInInspector] public bool isGrounded;

        [Header("FloorCheck")]
        [SerializeField] Transform checkFloor;
        [SerializeField] Vector3 jumpRange;
        [SerializeField] LayerMask floorLayer;


        //CONTROL MEMORY//
        bool ctrlJumpDown;
        bool ctrlJumpUp;

        public CameraFollow pcamera;

        [Header("NEW JUMP MODS")]
        [SerializeField] float lowJumpMultiplier = 2f;
        [SerializeField] float fallMultiplier = 2.5f;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }


        void Update()
        {
#if UNITY_EDITOR
            Application.targetFrameRate = targetFrameRate;
#endif
            Controls();
        }
        void FixedUpdate()
        {
            Move();
            GravityMods();
            BuildJump();
            //AnimatorController();
        }

        private void Controls()
        {
            move = Input.GetAxisRaw("Horizontal");
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                this.transform.eulerAngles = Vector3.zero;
            }

            if (Input.GetButtonDown("Jump"))
            {
                ctrlJumpDown = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                ctrlJumpUp = true;
            }
        }

        void Move()
        {
            float _move = move * maxSpeed;
            Vector3 targetVelocity = new Vector3(_move, rb.velocity.y, rb.velocity.z);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refMovementVector, accelerationTime);
        }

        void BuildJump()
        {
            //comprueba si hay un suelo debajo del personaje
            isGrounded = Physics.CheckBox(checkFloor.position, jumpRange, Quaternion.identity, floorLayer);



            if (ctrlJumpDown) //JUMP REMEMEMBER
            {
                fJumpPressedRemember = fJumpPressedRememberTime;
            }

            //if (ctrlJumpUp && fCutJumpTimeRemember > 0) //CUT JUMP
            //{
            //    if (rb.velocity.y > 0)
            //    {
            //        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fCutJumpHeight);
            //    }
            //}

            if (isGrounded) //GROUNDED VARIABLES UPDATE
            {
                currentExtraJumps = maxExtraJumps;
                fGroundedRemember = fGroundedRememberTime;
            }

            if (fJumpPressedRemember > 0 && fGroundedRemember > 0) //JUMP//JUMP//JUMP//
            {
                fJumpPressedRemember = 0;
                fGroundedRemember = 0;
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
            fGroundedRemember -= Time.fixedDeltaTime;
            fCutJumpTimeRemember -= Time.fixedDeltaTime;
            ctrlJumpDown = false;
            ctrlJumpUp = false;
        }

        private void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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


        private void OnDrawGizmosSelected()
        {
            //Draws a cuebe in groundcheck area
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(checkFloor.position, jumpRange);
        }

        //void AnimatorController()
        //{

        //    if (isJump)
        //    {
        //        animator.Play("Jump");
        //    }

        //    if (rb.velocity.y < 0)
        //    {
        //        animator.Play("Fall");
        //        isJump = false;
        //    }
        //    else
        //    {
        //        if (!isJump)
        //        {
        //            if (rb.velocity.x == 0)
        //            {
        //                animator.Play("Idle");
        //            }
        //            else
        //            {
        //                animator.Play("Walk");
        //            }
        //        }
        //    }

        //}

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.CompareTag("Enemy"))
        //    {
        //        this.enabled = false;
        //        //rb.isKinematic = true;
        //        rb.velocity = Vector2.zero;
        //        animator.Play("Dying");
        //    }

        //    if (collision.CompareTag("Key"))
        //    {
        //        Key Key = collision.GetComponent<Key>();
        //        Key.OnCollect();
        //        hasKey = true;
        //        portal.Activar();
        //    }

        //    if (collision.CompareTag("Portal") && hasKey)
        //    {
        //        pcamera.enabled = false;
        //        pcamera.Zoom();
        //        this.enabled = false;
        //        rb.isKinematic = true;
        //        rb.velocity = Vector2.zero;
        //    }
        //}

        //void EventAnimationDie()
        //{
        //    UnityEngine.SceneManagement.SceneManager.LoadScene
        //        (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        //}

    }
}
