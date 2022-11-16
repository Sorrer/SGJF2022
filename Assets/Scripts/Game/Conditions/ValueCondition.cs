using Game.Common.ScriptableData;
using UnityEngine;

namespace Game.Conditions
{
    [CreateAssetMenu(fileName = "Value Condition", menuName = CONDITION_BASE_FILE_PATH + "SO Value")]
    public class ValueCondition : ConditionBase
    {
        public ScriptableDataBase data1;
        public ScriptableDataBase data2;

        public override bool IsTrue()
        {
            return data1.Equals(data2);
        }
    }
}