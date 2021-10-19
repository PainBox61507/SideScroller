using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class PlayerDash : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] float dashSpeed;
        [SerializeField] float dashTime;
        [SerializeField] float dashExitTime;
        float MaxDashTime = 10;
        private bool isDashing;
        public int numberOfDashes = 1;
        [HideInInspector]public int currentNumberOfDashes;
        Vector2 direction;
        [Header("Dash Tweaks")]
        [SerializeField] float dashSpeedReturnal;
        [SerializeField] private float dashSlow = 0.5f; 
        [SerializeField] private float dashSlowDuration = 0.1f;

        //[SerializeField] float dashReductionTime = 0.2f;
        private Vector3 refMovementVector;

        [Header("Attack Settings")]
        [SerializeField] float damageAreaRadius;

        [Space]
        public bool isGoingThrougObjects = false;
        [SerializeField] LayerMask layer1;
        [SerializeField] LayerMask layer2;

        [SerializeField] Vector3 bodyArea;


        //List<Collider> lastEnemiesColliders;
        //List<Collider> enemiesColliders;

        [SerializeField] Transform dashColliders;
        PlayerVolumeTrigger playerVolumeTrigger;
        DashDamageTrigger dashDamageTrigger;

        PlayerAimController playerAimController;
        PlayerJumpController playerJumpController;
        CollisionManager collisionManager;
        PlayerMoveController playerMoveController;
        Rigidbody rb;

        PlayerAnimationController playerAnimationController;

        void Start()
        {
            playerVolumeTrigger = dashColliders.GetComponentInChildren<PlayerVolumeTrigger>();
            dashDamageTrigger = dashColliders.GetComponentInChildren<DashDamageTrigger>();
            playerVolumeTrigger.playerDash = this;
            dashDamageTrigger.playerDash = this;
            dashDamageTrigger.gameObject.SetActive(false);
            playerVolumeTrigger.gameObject.SetActive(false);
            collisionManager = GetComponent<CollisionManager>();
            collisionManager.TouchedTheGround += ReloadDash;
            playerAimController = GetComponent<PlayerAimController>();
            playerJumpController = GetComponent<PlayerJumpController>();
            playerMoveController = GetComponent<PlayerMoveController>();
            rb = GetComponent<Rigidbody>();
            playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        }

        void Update()
        {
            if(collisionManager.groundedRemember > 0 && !isDashing)
            {
                currentNumberOfDashes = numberOfDashes;
              //  Debug.Log("DashReset");
            }
            if (Input.GetButtonDown("Ability") && !isDashing && currentNumberOfDashes > 0)
            {
                currentNumberOfDashes--;
                Dash(playerAimController.Direction);
            }
        }

        private void ReloadDash()
        {
            if (!isDashing)
            {
            currentNumberOfDashes = numberOfDashes;
            }
        }


        private void Dash(Vector2 _direction)
        {
                direction = _direction.normalized;
                rb.velocity = direction * dashSpeed;
                StartCoroutine(Dash());
        }



        IEnumerator Dash()
        {
            //---------------DASH BEGIN--------------
            isDashing = true;
            rb.useGravity = false;
            playerJumpController.enabled = false;
            playerMoveController.enabled = false;
            this.gameObject.layer = LayerConstants.DASH_LAYER;
            dashDamageTrigger.gameObject.SetActive(true);
            playerVolumeTrigger.gameObject.SetActive(true);
            playerAnimationController?.Dash(true);


            yield return DashRun();

            //--------------DASH END-----------------
            //rb.velocity = Vector3.SmoothDamp(rb.velocity,rb.velocity.normalized * playerMoveController.maxSpeed, ref refMovementVector, dashReductionTime);

            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * dashSlow, rb.velocity.z);
            isDashing = false;
            rb.useGravity = true;
            playerMoveController.enabled = true;
            playerJumpController.enabled = true; 
            this.gameObject.layer = LayerMask.NameToLayer("Player");
            dashDamageTrigger.gameObject.SetActive(false);
            playerVolumeTrigger.gameObject.SetActive(false);
            playerAnimationController?.Dash(false);

            yield return DashSlow(); 
        }

        IEnumerator DashRun()
        {
            float _countDash = dashTime;
            float _countDashThrough = 0;
            float _countMaxDashTime = MaxDashTime;
            while (_countDash > 0 || _countDashThrough >= 0 && _countMaxDashTime > 0)
            {
                yield return null;
                DrawPath(Color.green);
                isGoingThrougObjects = Physics.CheckBox(this.transform.position, bodyArea/2, Quaternion.identity, layer1 + layer2);

                if (rb.velocity.magnitude <= dashSpeedReturnal && isGoingThrougObjects)
                {
                    rb.velocity = direction * -dashSpeed;
                }

                _countDashThrough -= Time.deltaTime;
                _countDash -= Time.deltaTime;
                _countMaxDashTime -= Time.deltaTime;

                if (isGoingThrougObjects)
                {
                    if(currentNumberOfDashes <= 0)
                    {
                     currentNumberOfDashes = 1;
                    }
                    _countDashThrough = dashExitTime;
                }
            }
            DrawPath(Color.black);
        }

        IEnumerator DashSlow()
        {
            float _slowTimeRemaining = 0;

            Vector3 _velocityNormalized = rb.velocity.normalized;

            while (_slowTimeRemaining < dashSlowDuration)
            {
                if (collisionManager.IsGrounded)
                {
                    yield break;
                }
                if (rb.velocity.y > dashSlow)
                {
                DrawPath(Color.red);
                    rb.velocity = Vector3.SmoothDamp(rb.velocity,_velocityNormalized * dashSlow, ref refMovementVector, dashSlowDuration);
                    //float _newVelocity = rb.velocity.y - dashSlow * Time.deltaTime;
                    //print("Y new speed " + _newVelocity);
                    //if (Mathf.Abs(_newVelocity) < 0.001)
                    //{
                    //    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    //    continue;
                    //}
                   // rb.velocity = new Vector3(rb.velocity.x, _newVelocity, rb.velocity.z);
                }

                yield return null;
                _slowTimeRemaining += Time.deltaTime;
            }
            DrawPath(Color.black);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        //IEnumerator DashSlow()
        //{
        //    float _slowTimeRemaining = 0;

        //    while ( _slowTimeRemaining < dashSlowDuration)
        //    {

        //       if(rb.velocity.y > 0.001)
        //        {
        //            print("Y current speed " + rb.velocity);
        //         float _newVelocity = rb.velocity.y - dashSlow * Time.deltaTime;
        //            print("Y new speed " + _newVelocity);
        //            if (Mathf.Abs(_newVelocity) < 0.001) 
        //            {
        //                rb.velocity =new Vector3(rb.velocity.x, 0, rb.velocity.z);
        //                continue;
        //            }
        //            rb.velocity = new Vector3(rb.velocity.x, _newVelocity, rb.velocity.z);
        //        }

        //        yield return null;
        //        _slowTimeRemaining += Time.deltaTime;
        //    }
        //}


        private void DrawPath(Color _pathColor)
        {
#if UNITY_EDITOR
            _DrawPath( _pathColor);
#endif
        }

#if UNITY_EDITOR
        [Space]
        [Header("Debug")]
        [SerializeField]private bool drawPath = false;
        [SerializeField] private float pathSegmentsLength = 0.7f;
        [SerializeField] private float _time = 10f;
        private void _DrawPath(Color _pathColor)
        {
            if (drawPath)
            {
            Debug.DrawLine(transform.position, transform.position + rb.velocity.normalized * pathSegmentsLength, _pathColor,_time);
            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(this.transform.position, bodyArea);
        }

#endif
        //public IEnumerator DashAttack(float _seconds2Wait)
        //{
        //    float count = 0;
        //    while (count < _seconds2Wait)
        //    {

        //        //enemiesColliders.AddRange(Physics.OverlapSphere(this.transform.position, damageAreaRadius));


        //        //for (int i = 0; i < enemiesColliders.Count; i++)
        //        //{
        //        //    if (!enemiesColliders[i].CompareTag("Enemy"))
        //        //    {
        //        //        enemiesColliders.RemoveAt(i);
        //        //        i--;
        //        //    }
        //        //}
        //        //print($"{enemiesColliders.Count} enemies found");

        //        //for (int i = 0; i < enemiesColliders.Count; i++)
        //        //{
        //        //    bool _wasHitBefore = false;
        //        //    foreach (var _lasEnemyCollided in lastEnemiesColliders)
        //        //    {
        //        //        if(_lasEnemyCollided == enemiesColliders[i])
        //        //        {
        //        //            _wasHitBefore = true;
        //        //            break;
        //        //        }
        //        //    }
        //        //    if (!_wasHitBefore)
        //        //    {
        //        //    enemiesColliders[i].TryGetComponent<EnemyController>(out EnemyController _enemyController);
        //        //        _enemyController.RecieveDamage(attackType);
        //        //    }
        //        //}

        //        //enemiesColliders.Clear();
        //        //lastEnemiesColliders = enemiesColliders;

        //        yield return null;
        //        count += Time.deltaTime;
        //    }

        //}


    }
}

