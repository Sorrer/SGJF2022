using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Tracker", menuName = "Settings/LevelTrack")]
    public class LevelTracker : ScriptableObject
    {
        public List<LvlStat> levels = new List<LvlStat>();

        public void MoveLevel(int lvlTarget, int lvlDestination) {
            levels.Remove();
        }

    }
