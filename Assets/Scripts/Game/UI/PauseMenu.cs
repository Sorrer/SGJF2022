using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static PauseMenu instance;
        private static bool isPaused = false;
        public static bool IsPaused
        {
            get { return isPaused; }
            private set { isPaused = value; }
        }
        
        
        
        
        public float timeScalePause;
        
        
        public UnityEvent OnPause;

        public UnityEvent OnResume;

        
        private void Start()
        {
            timeScalePause = Time.timeScale;
            IsPaused = false;
        }

        private void Awake()
        {
            instance = this;
        }
        
        

        public static void Pause()
        {
            instance.pauseGame(); 
        }

        public static void TogglePause()
        {
            if (isPaused)
            {
                instance.Resume(); 
            }
            else
            {
                instance.pauseGame(); 
            }
        }

        public void pauseGame()
        {
            
            if (IsPaused) return;
            IsPaused = true;
            
            timeScalePause = Time.timeScale;
            Time.timeScale = 0;
            
            if(OnPause != null) OnPause?.Invoke();
        }

        public void Resume()
        {
            IsPaused = false;
            
            Time.timeScale = timeScalePause;
            
            if(OnResume != null) OnResume?.Invoke();
        }
    }
}