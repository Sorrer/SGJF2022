using UnityEngine;

namespace Game.Common.ScriptableData
{
    public class ScriptableDataIdSlotAttribute : PropertyAttribute
    {
        public string ScriptableObjectField { get; private set; }
        public string ScriptableObjectIdField { get; private set; }

        public ScriptableDataIdSlotAttribute(string scriptableObjectField, string scriptableObjectIdField = "Id")
        {
            ScriptableObjectField = scriptableObjectField;
            ScriptableObjectIdField = scriptableObjectIdField;
        }
    }
}