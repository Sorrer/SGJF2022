using UnityEngine;

namespace Game.Common.ScriptableData.Values
{
    [CreateAssetMenu(fileName = "Int Data", menuName = SCRIPTABLE_OBJECT_DATA_MENU_NAME + "Int")]
    public class IntScriptableData : ScriptableData<int>
    {
        public void Increment()
        {
            this.value++;
        }
    }
}