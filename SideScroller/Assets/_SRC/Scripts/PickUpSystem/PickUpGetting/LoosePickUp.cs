using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class LoosePickUp : CanPickUp
    {
        public PickUpController pickUp;
        public Rigidbody rb;

        private void Start()
        {
            pickUp = this.transform.parent.GetComponent<PickUpController>();
            rb = this.transform.parent.GetComponent<Rigidbody>();
        }

        public override PickUpController TakePickUp()
        {
            if (canPickUp)
            {
                rb.isKinematic = true;
                return pickUp;
            }

            return null;
        }

    }
}
