using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    public class PlayerAimController : MonoBehaviour
    {
        public float AngleFromRight { get; private set; }
        //public Vector3 PointInWorld { get; private set; }
        public Vector3 Direction { get; private set; } = Vector3.right;
        public Vector3 LastHorizontalDirection { get; private set; } = Vector3.right;

        private void Start()
        {
            ReadInput();
        }

        private void Update()
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //zPlane = new Plane(Vector3.forward, Vector3.zero);

            //if (zPlane.Raycast(ray, out float enter));
            //{
            //Vector3 hitPoint = ray.GetPoint(enter);
            //Direction = Vector3.Normalize(hitPoint - this.transform.position);
            //}
            ReadInput();



            //Debug.DrawLine(ray.origin, ray.GetPoint(enter),Color.red);
        }

        void ReadInput()
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {

                Direction = Vector3.Normalize(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
                if(Direction == Vector3.right || Direction == Vector3.left) LastHorizontalDirection = Direction;
                AngleFromRight = Vector2.SignedAngle(Vector2.right, Direction);

            }
            else
            {
                Direction = LastHorizontalDirection;
            }
        }
    }
}
