using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    static public class PlayerInput
    {

        static public float Horizontal()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        static public float Vertical()
        {
            return Input.GetAxisRaw("Vertical");
        }

        static public bool Jump()
        {
            return Input.GetButton("Jump");
        }

        static public bool Shoot()
        {
            return Input.GetButton("Shoot");
        }

        static public bool Ability()
        {
            return Input.GetButton("Ability");
        }
    }
}