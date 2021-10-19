using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    static class ObjectCallingAndDestroying
    {
        public static GameObject Summon(Object _Object, Vector3 _position, Quaternion _rotation)
        {
            return Object.Instantiate(_Object, _position, _rotation) as GameObject;
        }

        public static void Dismiss(Object _obj)
        {
            Object.Destroy(_obj);
        }


    }
}
