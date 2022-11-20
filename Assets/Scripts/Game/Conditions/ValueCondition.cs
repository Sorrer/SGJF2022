using Game.Common.ScriptableData;
using Game.Common.ScriptableData.Values;
using UnityEngine;

namespace Game.Conditions
{
    [CreateAssetMenu(fileName = "Value Condition", menuName = CONDITION_BASE_FILE_PATH + "SO Value")]
    public class ValueCondition : ConditionBase
    {
        public ScriptableDataBase data1;

        public enum Comparator
        {
            EQUAL, NOT_EQUAL, LESS_THAN, GREATER_THAN, LESS_THAN_EQUAL, GREATER_THAN_EQUAL
        }

        public float FloatCompare;
        
        public override bool IsTrue()
        {

            if (data1.GetType() == typeof(FloatScriptableData))
            {
                
            }

            return false;
        }
    }
}