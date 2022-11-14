using Game.ScriptableData.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Common.ScriptableData
{
    public abstract class ScriptableDataBase : UnityEngine.ScriptableObject, ISerializable<string>
    {
        [field: SerializeField] public UnityEvent PreSerialized { get; private set; } = new UnityEvent();
        [field: SerializeField] public UnityEvent Serialized { get; private set; } = new UnityEvent();
        [field: SerializeField] public UnityEvent PreDeserialized { get; private set; } = new UnityEvent();
        [field: SerializeField] public UnityEvent Deserialized { get; private set; } = new UnityEvent();
        [SerializeField][GUID] private string Id;

        [SerializeField] protected bool isSavable;

        protected bool
            fieldDirty; // Currently this is not used, but in the future if dirty data needs to be detected, it can.

        public string GetGuid()
        {
            return Id;
        }

        public string Serialize()
        {
            PreSerialized.Invoke();
            string result = InternalSerialize();
            Serialized.Invoke();
            return result;
        }

        public void Deserialize(string s)
        {
            PreDeserialized.Invoke();
            InternalDeserialize(s);
            Deserialized.Invoke();
        }

        protected abstract string InternalSerialize();

        protected abstract void InternalDeserialize(string s);

        public bool IsSavable()
        {
            return isSavable;
        }

        public bool IsDirty()
        {
            return fieldDirty;
        }

        public void ClearDirtyFlag()
        {
            fieldDirty = false;
        }
    }
}