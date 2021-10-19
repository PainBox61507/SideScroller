using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class DamageOnTouch : MonoBehaviour
    {
        [SerializeField]private float validAngle;

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
                {
                    Vector2 _direction = Vector3.Normalize(collision.transform.position - this.transform.position);
                    float _angle = Vector2.Angle(Vector2.down, _direction);
                    Rigidbody rb = collision.transform.GetComponent<Rigidbody>();

                    if (_angle < validAngle / 2)
                    {
                        playerController.RecieveDamage();
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Transform thisTransform = this.transform;

            var rotationRight = Quaternion.AngleAxis(validAngle / 2, Vector3.forward);
            var rotationLeft = Quaternion.AngleAxis(-validAngle / 2, Vector3.forward);
            var right = rotationRight * Vector3.down;
            var left = rotationLeft * Vector3.down;
            Debug.DrawLine(thisTransform.position, thisTransform.position + right, Color.red);
            Debug.DrawLine(thisTransform.position, thisTransform.position + left, Color.red);


            //Code To draw line with player in case of needing it
            //Vector2 _direction 
            //Debug.DrawLine(collision.transform.position, collision.transform.position + (Vector3)_direction, Color.magenta,5);
        }
    }

}