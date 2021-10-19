using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class AdjustedImpulse : Impulse
    {
        [Header("ImpulseAdjust")] [SerializeField] float ImpulseAdjust;
        protected override void ApplyImpulse()
        {
            float angle = transform.rotation.eulerAngles.z;
            if (angle != 90 && angle != 270)
            {
                if (angle < 270)
                {
                    if (angle < 90)
                    {
                        angle += ImpulseAdjust;
                    }
                    else
                    {
                        angle -= ImpulseAdjust;
                    }
                }
                else
                {
                    angle += ImpulseAdjust;
                }
                Quaternion newAngle = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = newAngle;
            }

            base.ApplyImpulse();
        }
    }

}