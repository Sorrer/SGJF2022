using System.Collections.Generic;
using UnityEngine;

namespace Game.Conditions
{
    [CreateAssetMenu(fileName = "Level Order Condition", menuName = CONDITION_BASE_FILE_PATH + "Level Order")]
    public class LevelCondition : ConditionBase
    {
        
        // TODO: Replace GameObject with your level scriptable object
        public List<GameObject> LevelOrder = new List<GameObject>();
        
        public override bool IsTrue()
        {
            return false; // TODO: Compare against current level order, if there is a level order that this exists
        }
    }
}