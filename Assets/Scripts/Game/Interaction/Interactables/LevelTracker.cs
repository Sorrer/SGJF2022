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

        public int GetLevelIndexOf(int levelNum)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].levelNum == levelNum) return i;
            }

            return -1;
        }

        public LvlStat GetLevel(int levelNum)
        {  
            
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].levelNum == levelNum) return levels[i];
            }

            return new LvlStat() { levelNum = -1 };
        }

        public void DisableLvl(int levelNum)
        {
            var lvlStatInd = GetLevelIndexOf(levelNum);
            var lvlStat = levels[lvlStatInd];
            
            
            lvlStat.isOpen = false;

            levels[lvlStatInd] = lvlStat;


        }

        public void EnableLvl(int levelNum)
        {
            
            var lvlStatInd = GetLevelIndexOf(levelNum);
            var lvlStat = levels[lvlStatInd];
            
            
            lvlStat.isOpen = true;

            levels[lvlStatInd] = lvlStat;

        }

        public LvlStat GetNextLevel(int currentLevelNum)
        {

            int ind = GetLevelIndexOf(currentLevelNum);

            if (ind + 1 >= levels.Count)
            {
                return new LvlStat(){ levelNum = -1};  
            }

            return levels[ind + 1];
        }

        public bool IsBefore(int levelNum, int targetLevelNum)
        {
            if (GetLevelIndexOf(levelNum) > GetLevelIndexOf(targetLevelNum))
            {
                return true;
            }

            return false;
        }

    }
