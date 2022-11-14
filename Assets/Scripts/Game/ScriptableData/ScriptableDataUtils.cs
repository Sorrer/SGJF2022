using UnityEngine;

namespace Game.Common.ScriptableData
{
    public class ScriptableDataUtils
    {
        public static TType DeserializeToNew<TType>(object data) where TType : ScriptableObject, ISerializable<object>
        {
            return DeserializeToNew<TType, object>(data);
        }

        public static TType DeserializeToNew<TType, TSerialized>(TSerialized data) where TType : ScriptableObject, ISerializable<TSerialized>
        {
            var instance = ScriptableObject.CreateInstance<TType>();
            instance.Deserialize(data);
            return instance;
        }
    }
}
