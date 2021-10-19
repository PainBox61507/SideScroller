using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class PlayerAnimationController : AnimationBaseController
    {
        public PlayerAnimationController(Animator _animator): base(_animator)
        {
            animator = _animator;
        }

        public void Walk(float _currentSpeed)
        {
            if (_currentSpeed > 0.01 || _currentSpeed < -0.01) return;  
               // print("Walking at " + _currentSpeed);
        }
        public void Idle()
        {
            //print("isIdle");
        }

        public void Jump()
        {
            //print("Jumped");
        }

        public void MidAir(bool isMidAir)
        {
            //print($"isMidAir {isMidAir}");
        }

        public void Dash(bool isDashing)
        {
           // print("isDash");
        }

        public void Land()
        {
            //print("Landed");
        }

        public void Shoot()
        {
            //print("Shot");
        }
    }
}
