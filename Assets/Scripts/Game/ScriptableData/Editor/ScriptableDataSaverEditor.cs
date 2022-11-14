using Game.Common.ScriptableData;
using UnityEditor;
using UnityEngine;

namespace Game.Common.ScriptableObject.ScriptableData.Editor
{
    [CustomEditor(typeof(ScriptableDataSaver))]
    public class ScriptableDataSaverEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var saver = (ScriptableDataSaver)target;

            if (GUILayout.Button("Refresh Database")) saver.RefreshDatabase();
        }
    }
}