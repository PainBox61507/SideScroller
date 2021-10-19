using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class Bounce : MonoBehaviour
    {
        [SerializeField] private AttackType attackType;
        [SerializeField] private float validAngle = 90;
        [SerializeField] private float bounceAmount = 4;
        private EnemyController enemyController;

        private void Start()
        {
            enemyController = this.GetComponent<EnemyController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                for (int i = 0; i < collision.contactCount; i++)
                {
                    Vector2 _direction = Vector3.Normalize(collision.GetContact(i).point - this.transform.position);
                    float _angle = Vector2.Angle(Vector2.up, _direction);
                    collision.transform.TryGetComponent<Rigidbody>(out Rigidbody _rb);

                    if (_angle < validAngle / 2)
                    {
                        enemyController.RecieveDamage(attackType);
                        _rb.velocity = new Vector2(_rb.velocity.x, bounceAmount);
                        return;
                    }
                }
            }
        }


        private void OnDrawGizmosSelected()
        {
            Transform thisTransform = this.transform;

            var rotationRight = Quaternion.AngleAxis(validAngle / 2, Vector3.forward);
            var rotationLeft = Quaternion.AngleAxis(-validAngle / 2, Vector3.forward);
            var right = rotationRight * Vector3.up;
            var left = rotationLeft * Vector3.up;
            Debug.DrawLine(thisTransform.position, thisTransform.position + right, Color.black);
            Debug.DrawLine(thisTransform.position, thisTransform.position + left, Color.black);

            //Code To draw line with player in case of needing it
            //Vector2 _direction 
            //Debug.DrawLine(collision.transform.position, collision.transform.position + (Vector3)_direction, Color.magenta,5);
        }

    }

}

