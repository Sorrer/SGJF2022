using System;
using UnityEngine;

namespace Game.Utility
{
    public class EnableLevelOnStart : MonoBehaviour
    {
        public int levelNum;
        public LevelTracker levelTracker;
        private void Awake()
        {
            levelTracker.EnableLvl(levelNum);
        }
    }
}