using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    [RequireComponent(typeof(CharacterController), typeof(CollisionManager))]
    public class PlayerCharControlExperimentalMovement : MonoBehaviour
    {
        private CharacterController charController;
        private CollisionManager collisionManager;
        private bool groundedPlayer;
        [SerializeField] private Vector3 playerVelocity;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float gravityValue;
        [SerializeField] private float playerSpeed;

        // Start is called before the first frame update
        void Start()
        {
            charController = GetComponent<CharacterController>();
            collisionManager = GetComponent<CollisionManager>();
        }

        // Update is called once per frame
        void Update()
        {
            Jump();
        }

        private void FixedUpdate()
        {
            
            groundedPlayer = collisionManager.IsGrounded;
        }

        void Jump()
        {
            print(groundedPlayer);
            //if (groundedPlayer && playerVelocity.y < 0)
            //{
            //    playerVelocity.y = 0f;
            //}

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            charController.Move(move * Time.deltaTime * playerSpeed);

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            charController.Move(playerVelocity * Time.deltaTime);
        }
    }
}