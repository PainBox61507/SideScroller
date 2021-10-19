using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    [RequireComponent(typeof(Rigidbody))]
    public class Impulse : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] float impulseStrength;
        [SerializeField] Vector3 impulseDirection = Vector3.right;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            ApplyImpulse();
        }

        protected virtual void ApplyImpulse()
        {
            rb.velocity = transform.rotation * impulseDirection * impulseStrength;
        }
    }

}
