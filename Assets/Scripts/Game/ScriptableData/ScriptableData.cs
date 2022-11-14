using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Game.Common.ScriptableData
{
    [Serializable]
    public abstract class ScriptableData<T> : ScriptableDataBase
    {
        public delegate void OnChangeEventHandler(T data);

        public const string SCRIPTABLE_OBJECT_DATA_MENU_NAME = "Scriptables/";

        [SerializeField]
        private T _value;

        public T value
        {
            get => _value;
            set
            {
                // TODO: Benchmark this performance and check if it is faster to serialize and flag than check this condition every single set
                // In theory if code is correctly set, this should not be necessary.
                if (_value != null && !_value.Equals(value))
                    if (isSavable)
                        fieldDirty = true;

                _value = value;
                InvokeChangeEvent(value);
            }
        }

        public event OnChangeEventHandler OnChangeEvent;

        protected void InvokeChangeEvent(T data)
        {
            try
            {
                OnChangeEvent?.Invoke(data);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                Debug.LogWarning("Failed to run On Change Event. Make sure to check what happened. Continuing.");
            }
        }

        protected override string InternalSerialize()
        {
            return JsonConvert.SerializeObject(_value);
        }

        protected override void InternalDeserialize(string s)
        {
            _value = JsonConvert.DeserializeObject<T>(s);
        }
    }

    public interface ISerializable<T>
    {
        public T Serialize();
        public void Deserialize(T s);
    }
}