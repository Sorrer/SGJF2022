using System.Collections.Generic;
using UnityEngine;

namespace Game.Common.ScriptableData
{
    [CreateAssetMenu(fileName = "Scriptable Data Group", menuName = "Scriptables/Group")]
    public class ScriptableDataGroup : ScriptableObject
    {
        public List<ScriptableDataBase> data = new List<ScriptableDataBase>();

        public void ResetToDefault()
        {
            foreach (var d in data)
            {
                d.ResetToDefault();
            }
        }
    }
}