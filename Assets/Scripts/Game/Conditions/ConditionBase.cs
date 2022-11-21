using System.Collections.Generic;
using UnityEngine;

namespace Game.Conditions
{
    
    public abstract class ConditionBase : ScriptableObject
    {
        
        public const string CONDITION_BASE_FILE_PATH = "Condition/"; 
        public abstract bool IsTrue();

        public static bool IsTrueAll(List<ConditionBase> conditionBases, bool useOr = false)
        {
            if (conditionBases.Count == 0) return true;

            foreach (var cond in conditionBases)
            {
                if (useOr)
                {
                    if (cond.IsTrue()) return true;
                }
                else
                {
                    if (!cond.IsTrue()) return false;
                }
            }
            
            return !useOr;
        }
    }
}