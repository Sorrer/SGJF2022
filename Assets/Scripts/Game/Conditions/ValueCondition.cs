using System;
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

        public Comparator comparison;
        public float FloatCompare;
        public int IntCompare;
        public bool BooleanCompare;

        public override bool IsTrue()
        {

            if (data1.GetType() == typeof(FloatScriptableData))
            {
                var targetVal = FloatCompare;
                var curVal = ((FloatScriptableData)data1).value;
                switch (comparison)
                {

                    case Comparator.EQUAL:
                        return Math.Abs(targetVal - curVal) < float.Epsilon;
                    case Comparator.NOT_EQUAL:
                        return Math.Abs(curVal - targetVal) > float.Epsilon;
                    case Comparator.LESS_THAN:
                        return curVal < targetVal;
                    case Comparator.GREATER_THAN:
                        return curVal > targetVal;
                    case Comparator.LESS_THAN_EQUAL:
                        return curVal <= targetVal;
                    case Comparator.GREATER_THAN_EQUAL:
                        return curVal >= targetVal;
                    default:
                        Debug.Log("Failed to do comparison, but could not comparator");
                        return false;
                }

            }
            else if (data1.GetType() == typeof(IntScriptableData))
            {
                var curVal = ((IntScriptableData)data1).value;
                var targetVal = IntCompare;
                switch (comparison)
                {

                    case Comparator.EQUAL:
                        return curVal == targetVal;
                    case Comparator.NOT_EQUAL:
                        return curVal != targetVal;
                    case Comparator.LESS_THAN:
                        return curVal < targetVal;
                    case Comparator.GREATER_THAN:
                        return curVal > targetVal;
                    case Comparator.LESS_THAN_EQUAL:
                        return curVal <= targetVal;
                    case Comparator.GREATER_THAN_EQUAL:
                        return curVal >= targetVal;
                    default:
                        Debug.Log("Failed to do comparison, but could not comparator");
                        return false;
                }
            }
            else if (data1.GetType() == typeof(BoolScriptableData))
            {
                var targetVal = BooleanCompare;
                var curVal = ((BoolScriptableData)data1).value;
                return targetVal == curVal;
            }

            return false;
        }
    }
}