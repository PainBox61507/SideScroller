using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class CameraFollow : MonoBehaviour
    {

        public Transform player;
        Vector3 cameraOffset;

        //Animator animator;

        Vector3 currentPosition;
        Vector3 finalPosition;
        [Range(0, 1)] public float cameraSpeed;

        void Start()
        {
            //animator = GetComponent<Animator>();
            cameraOffset = player.position - this.transform.position;
            transform.parent = null;
        }

        private void FixedUpdate()
        {
            currentPosition = this.transform.position;
            finalPosition = player.position - cameraOffset;
            this.transform.position = Vector3.Lerp(currentPosition, finalPosition, cameraSpeed);
        }
    }

}