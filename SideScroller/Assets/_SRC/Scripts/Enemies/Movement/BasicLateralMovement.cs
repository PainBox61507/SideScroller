using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class BasicLateralMovement : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] float speed;
        [SerializeField] Vector2 direction = new Vector2(-1, 0);

        // Start is called before the first frame update
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            direction = this.transform.right;
        }


        void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Flag"))
            {
                // this.transform.eulerAngles += new Vector3(0, 180, 0);
                direction *= -1;
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            float move = direction.x * speed;
            rb.velocity = new Vector3(move, rb.velocity.y, rb.velocity.z);
        }

    }
}