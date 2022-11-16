using System;
using UnityEngine;

namespace Game.Common.ScriptableData.Utility
{
    /// <summary>
    /// Used to test before the level system
    /// </summary>
    public class ResetScriptableDataGroup : MonoBehaviour
    {
        public ScriptableDataGroup group;

        private void Start()
        {
            if(group != null) group.ResetToDefault();
        }
    }
}