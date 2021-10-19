using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{
    struct LayerConstants
    {
        public const int GROUND = 6;
        public const int NAVIGABLE = 7;
        public const int ENEMY = 8;
        public const int ENEMY_PROJECTILE = 9;
        public const int DASH_LAYER = 10;
        public const int PLAYER = 11;
        public const int UI_WORLD_SPACE = 12;

        public static LayerMask numToLayerMask(int _layerNumber)
        {
           return LayerMask.GetMask(LayerMask.LayerToName(_layerNumber));
        }
    }
}
