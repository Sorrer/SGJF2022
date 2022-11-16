using UnityEngine;

namespace Game.Conditions
{
    
    public abstract class ConditionBase : ScriptableObject
    {
        
        public const string CONDITION_BASE_FILE_PATH = "Condition/"; 
        public abstract bool IsTrue();
    }
}