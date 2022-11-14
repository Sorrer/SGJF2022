using UnityEngine;

namespace Game.Common.ScriptableData
{
    [CreateAssetMenu(fileName = "Scriptable Event", menuName = "Scriptables/Event/Event")]
    public class ScriptableEvent : UnityEngine.ScriptableObject
    {
        public delegate void InvokeEventHandler();

        private InvokeEventHandler invokeEvent;


        // TODO: Keep an eye on this, and see if this creates and undefined behaviors
        private void OnEnable()
        {
            ClearEvents();
        }

        private void OnDestroy()
        {
            ClearEvents();
        }


        // TODO: Find a safer way to handle and maybe garbage collect event handlers
        // EVENTS ARE HANDLED ON RUN TIME, WHEN THIS SCRIPTABLE OBJECT STARTS, IT SHOULD CLEAR
        public void AddEvent(InvokeEventHandler eventHandler)
        {
            invokeEvent += eventHandler;
        }

        public void RemoveEvent(InvokeEventHandler eventHandler)
        {
            invokeEvent -= eventHandler;
        }

        private void ClearEvents()
        {
            invokeEvent = null;
        }

        public void Invoke()
        {
            invokeEvent?.Invoke();
        }
    }
}