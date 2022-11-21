using System;
using System.Collections.Generic;
using Game.Common.Interactable.Interactables;
using UnityEngine;

namespace Game.Interaction.Interactables
{
    [CreateAssetMenu(fileName = "Level Tracker", menuName = "Settings/LevelTrack")]
    public class LevelTracker : ScriptableObject
    {
        
        [Serializable]
        public struct LvlStat {
            public int levelNum;
            public bool isOpen;
            public String sceneName;
        }

        [SerializeField]
        public static List<LevelStatusSO> levels = new List<LevelStatusSO>();

        LevelStatusSO tempTarget;

        private int indexTarget, indexDest;

        public void ClearLevels()
        {
            levels = new List<LevelStatusSO>();
        }
        public void MoveLevel(LevelStatusSO statTarget, LevelStatusSO statDestination, bool before) {
            
            Debug.Log("Move level before");
            print();
            
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
            levels = new List<LevelStatusSO>(levels);
            Debug.Log("Move level after");
            print();
            
        }

        public void print()
        {
            foreach (LevelStatusSO stat in levels)
            {
                Debug.Log("Stat data - " + stat.isOpen + " " + stat.levelNum + " " + stat.sceneName);
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

        public LevelStatusSO GetLevel(int levelNum)
        {  
            
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].levelNum == levelNum) return levels[i];
            }

            return null;
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

        public LevelStatusSO GetNextLevel(int currentLevelNum)
        {

            int ind = GetLevelIndexOf(currentLevelNum);

            if (ind + 1 >= levels.Count)
            {
                return null;
            }

            return levels[ind + 1];
        }

        public bool IsBefore(int levelNum, int targetLevelNum)
        {
            if (GetLevelIndexOf(levelNum) > GetLevelIndexOf(targetLevelNum))
            {
                return false;
            }

            return true;
        }

    }
}
