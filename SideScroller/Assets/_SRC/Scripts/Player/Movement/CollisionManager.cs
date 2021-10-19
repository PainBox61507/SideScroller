using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class CollisionManager : MonoBehaviour
    {
        [Header("FloorCheckSettings")]
        [SerializeField] float positionHeight;
        [SerializeField] Vector3 floorCheckArea;
        [SerializeField] Vector3 floorCenterCheckArea;
        [SerializeField] LayerMask floorLayer = LayerConstants.GROUND;
        [SerializeField] LayerMask floorLayer2 = LayerConstants.NAVIGABLE;
        [Header("QOL")]
        [SerializeField] float groundedRememberTime = 0.08f;
        [HideInInspector] public float groundedRemember;

        [Header("WallCheckSettings")]
        [SerializeField] Vector3 wallCheckArea;
        [SerializeField] float wallCheckSeparation;

        public delegate void DelegateTouchedTheGround();
        public event DelegateTouchedTheGround TouchedTheGround;

        bool isFloorCheckBox;

        bool wasGroundeLastUpdate = true;

        public bool IsGrounded { get; private set; }
        public bool isFloorCenterCheckBox { get; private set; }
        public bool IsRightWallded { get; private set; }
        public bool IsLeftWallded { get; private set; }

        PlayerAnimationController animationPlayerController;

        private void Start()
        {
            animationPlayerController = GetComponentInChildren<PlayerAnimationController>();
        }

        void FixedUpdate()
        {
            isFloorCheckBox = Physics.CheckBox(this.transform.position + Vector3.up * positionHeight, floorCheckArea/2, Quaternion.identity, floorLayer + floorLayer2);
            isFloorCenterCheckBox = Physics.CheckBox(this.transform.position + Vector3.up * positionHeight, floorCenterCheckArea/2, Quaternion.identity, floorLayer + floorLayer2);
            IsRightWallded = Physics.CheckBox(this.transform.position + Vector3.right * wallCheckSeparation, wallCheckArea/2, Quaternion.identity, floorLayer + floorLayer2);
            IsLeftWallded = Physics.CheckBox(this.transform.position + Vector3.left * wallCheckSeparation, wallCheckArea/2, Quaternion.identity, floorLayer + floorLayer2);

            if(isFloorCheckBox && !isFloorCenterCheckBox && (IsRightWallded || IsLeftWallded))
            {
                IsGrounded = false;
            }
            else
            {
                IsGrounded = isFloorCheckBox;
            }

            if (IsGrounded) //GROUNDED VARIABLES UPDATE
            {
                if (!wasGroundeLastUpdate)
                {
                    animationPlayerController.Land();
                }
                TouchedTheGround?.Invoke();
                groundedRemember = groundedRememberTime;
            }
            else
            {
                groundedRemember -= Time.fixedDeltaTime;
            }

            wasGroundeLastUpdate = IsGrounded;
        }



        #if UNITY_EDITOR
        [SerializeField] bool showGizmos;
        private void OnDrawGizmosSelected()
        {
            if (showGizmos)
            {
            //Draws a cubes in groundcheck area
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(this.transform.position + Vector3.up * positionHeight, floorCheckArea);
            Gizmos.DrawWireCube(this.transform.position + Vector3.up * positionHeight, floorCenterCheckArea);
            Gizmos.DrawWireCube(this.transform.position + Vector3.right * wallCheckSeparation, wallCheckArea);
            Gizmos.DrawWireCube(this.transform.position + Vector3.left * wallCheckSeparation, wallCheckArea);
            }
        }
        #endif
    }
}
