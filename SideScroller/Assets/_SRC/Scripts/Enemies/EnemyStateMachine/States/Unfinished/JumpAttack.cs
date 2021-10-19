using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class JumpAttack : EnemyState
    {

        Rigidbody rb;

        //[SerializeField] float coolDown = 0.5f;

        [SerializeField] float chargeTime = 0.4f;

        [SerializeField] float jumpForce = 13;
        [SerializeField] float jumpAngle = 55;

        [SerializeField] private float validLethalAngle = 270;


        [SerializeField] float checkGroundCenter = -0.5f;
        [SerializeField] Vector3 checkGroundArea = Vector3.one * 0.2f;

        LayerMask groundLayer1;
        LayerMask groundLayer2;
        
        private bool isJumping;
        private bool isGrounded;
        private bool isAttacking;



        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            groundLayer1 = LayerConstants.numToLayerMask(LayerConstants.NAVIGABLE);
            groundLayer2 = LayerConstants.numToLayerMask(LayerConstants.GROUND);
        }

        public override void FixedUpdateState()
        {
            if (isJumping )
            {
                if (IsGrounded() && IsFalling())
                {
                    isJumping = false;
                    isAttacking = false;
                    StateFinished();
                    //print("landed");
                    //print(rb.velocity);
                }
            }
        }

        public override bool IsAvailable()
        {
            bool _isAvailable = IsGrounded();
            //print(IsGrounded);
            return _isAvailable;
        }

        private bool IsFalling()
        {
                return rb.velocity.y <= 0;
        }

        private bool IsGrounded()
        {
            isGrounded =  Physics.CheckBox(this.transform.position + Vector3.up * checkGroundCenter, checkGroundArea / 2, Quaternion.identity, groundLayer1 + groundLayer2);
            //Debug.Log($"is grounded = {isGrounded}");
            return isGrounded;
        }

        public override bool IsReadyToChange()
        {
            bool isReadyToChange = IsAvailable() && !isAttacking;
            //print($"isReadyToChange = {isReadyToChange}");
            return isReadyToChange;
            //  throw new System.NotImplementedException();
        }

        public override void StartState()
        {
            rb.velocity = Vector3.zero;
            StartCoroutine(JumpingSecuence());
        }

        private IEnumerator JumpingSecuence()
        {
            Charging();
            yield return new WaitForSeconds(chargeTime);
            Jump();
        }

        private void Charging()
        {
            isAttacking = true;
        }

        private void Jump()
        {
            //print("jump!!");
            Vector3 direction = CalcDirection();
            //print(direction * jumpForce);
            // rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            rb.velocity = direction * jumpForce;
            isJumping = true;
        }

        
        #region
        private void OnCollisionStay(Collision collision)
        {
            if (isJumping && collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
                {
                    Vector2 _direction = Vector3.Normalize(collision.transform.position - this.transform.position);
                    float _angle = Vector2.Angle(Vector2.down, _direction);
                    Rigidbody rb = collision.transform.GetComponent<Rigidbody>();

                    if (_angle < validLethalAngle / 2)
                    {
                        playerController.RecieveDamage();
                    }
                }
            }
        }


        #endregion

        private Vector3 CalcDirection()
        {
            var _rot = Quaternion.AngleAxis(jumpAngle, Vector3.forward);
            Vector3 _direction = _rot * Vector3.right;
            return _direction;
        }
        public override void StopState()
        {
        }

        public override void UpdateState()
        {
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(this.transform.position + Vector3.up * checkGroundCenter, checkGroundArea);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, this.transform.position + CalcDirection() * jumpForce/3);

            //----------------------------Lethal collision angle------------------------
            Transform thisTransform = this.transform;

            var rotationRight = Quaternion.AngleAxis(validLethalAngle / 2, Vector3.forward);
            var rotationLeft = Quaternion.AngleAxis(-validLethalAngle / 2, Vector3.forward);
            var right = rotationRight * Vector3.down;
            var left = rotationLeft * Vector3.down;
            Debug.DrawLine(thisTransform.position, thisTransform.position + right, Color.red);
            Debug.DrawLine(thisTransform.position, thisTransform.position + left, Color.red);


            //Code To draw line with player in case of needing it
            //Vector2 _direction 
            //Debug.DrawLine(collision.transform.position, collision.transform.position + (Vector3)_direction, Color.magenta,5);
        }

        protected override void InstantiateAnimatorController(Transform animatorTransform, Animator animator)
        {
        }
    }


}
