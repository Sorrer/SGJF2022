using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Tracker", menuName = "Settings/LevelTrack")]
    public class LevelTracker : ScriptableObject
    {
        public List<LvlStat> levels = new List<LvlStat>();

        LvlStat tempTarget;

        private int indexTarget, indexDest;

        public void MoveLevel(LvlStat statTarget, LvlStat statDestination, bool before) {
            // remove level of number lvlTarget, save in temp
            tempTarget = statTarget;

            //Debug.Log(statTarget.levelNum + " " + levels.IndexOf(statTarget));
            //Debug.Log(statDestination.levelNum + " " + levels.IndexOf(statDestination));
            levels.Remove(statTarget);

            
            if (before) {
                levels.Insert(levels.IndexOf(statDestination), tempTarget);
            } else {
                levels.Insert(levels.IndexOf(statDestination)+1, tempTarget);
            }
        }

    }
