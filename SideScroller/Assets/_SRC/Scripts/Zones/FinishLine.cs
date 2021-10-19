using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiloZitare
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] bool useManualLvlSetting = false;
        [SerializeField] int level2Travel2;
        private void OnTriggerEnter(Collider other)
        {
            NextScene();
        }

        void NextScene()
        {
            Scene _scene;
            if (!useManualLvlSetting)
            {
                int currentLevelNumber = GetNumberFromString(SceneManager.GetActiveScene().name);
                Debug.Log($"Current level is level {currentLevelNumber}");
                string _nextLevelName = $"Level {currentLevelNumber + 1}";
                _scene = SceneManager.GetSceneByName(_nextLevelName);
                print(_nextLevelName);
                print(_scene.name);
                LoadScene(_scene);
            }
            else
            {
                _scene = SceneManager.GetSceneByName("Level " + level2Travel2);
                LoadScene(_scene);
            }
        }

        void LoadScene(Scene _scene)
        {
            if (_scene.IsValid())
            {
                SceneManager.LoadScene("Level " + _scene);
            }
            else
            {
                SceneManager.LoadScene("Level " + 1);

            }
        }

        int GetNumberFromString(string sentence)
        {
            int stringNumber;
            int.TryParse(sentence.Substring(5), out stringNumber);
            return stringNumber;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                if (level2Travel2 - 2 > 0)
                {
                    print("PrevScene");
                    SceneManager.LoadScene("Level " + (level2Travel2 - 2));
                }
                else
                {
                    if (level2Travel2 - 2 == -1)
                    {
                        SceneManager.LoadScene("Level " + 10);
                    }
                    else
                    {
                        SceneManager.LoadScene("Level " + 11);

                    }
                }
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                print("NextScene");
                //SceneManager.LoadScene("Level " + level2Travel2);
                NextScene();
            }
        }
    }
}
