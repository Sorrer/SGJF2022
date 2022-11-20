using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utility
{
    public class LevelLoader : MonoBehaviour
    {
        public static LevelLoader instance; 
        public int sceneWaitTime;

        public string loadingSceneName;

        private bool isLoading = false;
        
        public void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        public static void LoadLevel(string sceneName)
        {
            if (instance.isLoading)
            {
                Debug.LogError("Cannot load level when already loading level");
                return;
            }

            instance.StartCoroutine(instance.LoadingCoroutine(sceneName));

        }

        public static void LoadLevelSelect()
        {
            
        }

        public IEnumerator LoadingCoroutine(string sceneName)
        {
            isLoading = true;

            SceneManager.LoadScene(loadingSceneName);

            yield return null;
            
            // TODO: Fade in rat
            
            var loading = SceneManager.LoadSceneAsync(sceneName);

            loading.allowSceneActivation = false;
            
            yield return new WaitForSecondsRealtime(sceneWaitTime);

            
            while (!loading.isDone)
            {
                if (loading.progress >= 0.9f)
                {
                    loading.allowSceneActivation = true;
                }

                yield return null;
            }
            
            // TODO: Hide rat
            
            isLoading = false;

            yield return null;
        }
        
        
        
        
    }
}