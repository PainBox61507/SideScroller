using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiloZitare
{
    public class Wait
    {
        public static IEnumerator Seconds(float _seconds2Wait)
        {
            for (float count = 0; count < _seconds2Wait; count += Time.deltaTime)
            {
                yield return null;
            }
        }

    }

}
