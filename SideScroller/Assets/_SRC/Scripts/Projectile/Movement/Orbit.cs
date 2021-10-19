using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class Orbit : MonoBehaviour
    {
        [SerializeField] string objectiveTag;
        [SerializeField] float atractionRadius;
        [SerializeField] float atractionStrength;
        [SerializeField] ForceMode forceMode;
        Rigidbody rb;
        Collider[] Colliders;

        // Start is called before the first frame update
        void Start()
        {
            rb = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Colliders = Physics.OverlapSphere(this.transform.position, atractionRadius);
            foreach (var collider in Colliders)
            {
                if (collider.CompareTag(objectiveTag))
                {
                    Vector3 direction = (collider.transform.position - this.transform.position).normalized;
                    rb.AddForce(direction * atractionStrength, forceMode);
                }
            }
        }

        private void OnDisable()
        {
            Colliders = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, atractionRadius);
        }
    }
}
