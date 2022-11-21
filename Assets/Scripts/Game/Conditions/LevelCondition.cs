using System.Collections.Generic;
using Game.Interaction.Interactables;
using UnityEngine;

namespace Game.Conditions
{
    [CreateAssetMenu(fileName = "Level Order Condition", menuName = CONDITION_BASE_FILE_PATH + "Level Order")]
    public class LevelCondition : ConditionBase
    {
        public LevelTracker levelTracker;
        public List<int> LevelOrder = new List<int>();
        
        public override bool IsTrue()
        {

            if (LevelOrder.Count == 0)
            {
                Debug.LogError("LevelOrder condition not set, should be set or else is always true");
                return true;
            }
            
            for (int i = 0; i < LevelOrder.Count - 1; i++)
            {
                if (!levelTracker.IsBefore(LevelOrder[i], LevelOrder[i + 1]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}