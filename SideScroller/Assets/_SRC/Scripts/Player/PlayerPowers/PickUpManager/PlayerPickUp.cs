using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiloZitare
{
    [RequireComponent(typeof(PlayerAimController))]
    public class PlayerPickUp : MonoBehaviour
    {
        PlayerAimController playerAimController;
        PickUpController pickUp;
        [Space]
        //[SerializeField] float upIncreace;

        PlayerAnimationController playerAnimationController;


        private void Start()
        {
            playerAimController = GetComponent<PlayerAimController>();
            playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        }
        void Update()
        {
            if (Input.GetButtonDown("Shoot"))
            {
                if (pickUp != null)
                {
                    pickUp.Use(playerAimController.AngleFromRight);
                    playerAnimationController?.Shoot();
                }

            }
        }

        public void DetachPickUp()
        {
            pickUp = null;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.transform.CompareTag("PickUp"))
            {
                if (pickUp == null)
                {
                    CanPickUp canPickUp = collider.GetComponent<CanPickUp>();
                    PickUpController _pickUp = canPickUp.TakePickUp();
                    if (_pickUp != null)
                    {
                        _pickUp.Equip(this.transform, DetachPickUp);
                        pickUp = _pickUp;
                    }

                }
            }
        }
    }
}
