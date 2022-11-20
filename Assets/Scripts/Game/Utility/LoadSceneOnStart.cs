using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utility
{
    public class LoadSceneOnStart : MonoBehaviour
    {
        public string SceneName;
        public LevelTracker levelTracker; // TEMP HERE< NEED FAST CODE

        public void Start()
        {
            levelTracker.levels.Clear();
            SceneManager.LoadScene(SceneName);
        }
    }
}