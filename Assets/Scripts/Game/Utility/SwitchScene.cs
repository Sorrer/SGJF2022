using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utility
{
    public class SwitchScene : MonoBehaviour
    {

        public void Switch(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}