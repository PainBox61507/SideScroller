using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] string objectiveTag = "Enemy";
        [SerializeField] float magnetRadius = 2;
        [SerializeField] float magnetStrength = 30;
        [SerializeField] ForceMode forceMode = ForceMode.Acceleration;
        Rigidbody rb;
        List<Collider> colliders = new List<Collider>();
        Collider objetive;

        // Start is called before the first frame update
        void Start()
        {
            rb = this.GetComponent<Rigidbody>();
        }
        private void OnDisable()
        {
         objetive = null;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (objetive == false)
            {
                colliders.Clear();
                colliders.AddRange(Physics.OverlapSphere(this.transform.position, magnetRadius));
                foreach (var collider in colliders)
                {
                    if (collider.CompareTag(objectiveTag))
                    {
                        objetive = collider;
                        break;
                    }
                }
            }
            else
            {
                Vector3 direction = (objetive.transform.position - this.transform.position).normalized;
                rb.AddForce(direction * magnetStrength, forceMode);
            }
        }

        private void OnDrawGizmos()
        {
            if(objetive != null)
            {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, objetive.transform.position);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(this.transform.position, magnetRadius);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, magnetRadius);
        }
    }
}