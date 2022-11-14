namespace Game.Common.ScriptableData
{
    /// <summary>
    /// Do not use this abstract class for public member implementations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ScriptableEventValue<T> : UnityEngine.ScriptableObject
    {
        public delegate void InvokeEventHandler(T t);

        public const string SCRIPTABLE_CREATION_MENU_PATH = "Scriptables/Event/";

        private InvokeEventHandler InvokeEvent;


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
            InvokeEvent += eventHandler;
        }

        public void RemoveEvent(InvokeEventHandler eventHandler)
        {
            InvokeEvent -= eventHandler;
        }

        private void ClearEvents()
        {
            InvokeEvent = null;
        }

        public void Invoke(T t = default)
        {
            InvokeEvent?.Invoke(t);
        }
    }
}