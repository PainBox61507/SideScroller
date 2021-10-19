using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController characterController;
        private Vector3 refMovementVector;

        [Header("MovementVariables")]
        [SerializeField] private float maxSpeed = 6.5f;
        [SerializeField] private float accelerationTime = 0.1f;
        private float moveInput = 0;
        private float refSpeed;
        private float currentSpeed = 0;

        // Start is called before the first frame update
        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            Controls();
            Move();
        }

        private void Controls()
        {
            moveInput = PlayerInput.Horizontal();
        }

        private void Move()
        {
            //Vector3 _velocity = Vector3.SmoothDamp(Vector3.right * characterController.velocity.x, _targetVelocity, ref refMovementVector, accelerationTime);

            float _move = moveInput * maxSpeed * Time.deltaTime;
            currentSpeed = Mathf.SmoothDamp(currentSpeed,_move, ref refSpeed, accelerationTime);
            Vector3 _motion = new Vector3(currentSpeed, 0, 0);
            characterController.Move(Vector3.right * _move);

            print(" CurrentSpeed: " + currentSpeed +" Intention: "+ _move);
        }
    }
}