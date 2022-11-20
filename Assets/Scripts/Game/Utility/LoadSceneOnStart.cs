using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utility
{
    public class LoadSceneOnStart : MonoBehaviour
    {
        public string SceneName;

        public void Start()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}