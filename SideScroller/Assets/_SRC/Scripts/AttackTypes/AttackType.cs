using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    [CreateAssetMenu(fileName = "AttackType", menuName = "ScriptableObjects/AttackType", order = 1)]
    public class AttackType : ScriptableObject
    {
        public string attackName;
        public Sprite attackSprite;
    }

}
