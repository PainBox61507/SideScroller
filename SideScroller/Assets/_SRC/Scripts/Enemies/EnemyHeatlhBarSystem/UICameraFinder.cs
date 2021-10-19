using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiloZitare
{

    public class UICameraFinder : MonoBehaviour
    {
        Canvas canvas;
        // Start is called before the first frame update
        void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        private void Start()
        {

            foreach (Camera _cam in Camera.allCameras)
            {
                if (_cam.CompareTag(TagConstants.UI_WORLD_SPACE_CAMERA)) 
                {
                    canvas.worldCamera = _cam;
                    break;
                }
            }
        } 
    }
}