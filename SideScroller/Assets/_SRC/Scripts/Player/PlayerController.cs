using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiloZitare
{

    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Kill();
            }
        }

        public void RecieveDamage()
        {
            Kill();
        }
        public void Kill()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
