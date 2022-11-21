using System;
using Game.Interaction.Interactables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utility
{
    public class LoadSceneOnStart : MonoBehaviour
    {
        public string SceneName;
        public LevelTracker levelTracker; // TEMP HERE< NEED FAST CODE

        private void Awake()
        {
            levelTracker.ClearLevels();
        }

        public void Start()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}