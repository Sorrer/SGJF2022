using Game.Common.ScriptableData;
using Game.Common.Interactable;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Common.Interactable.Interactables
{
    /// <summary>
    /// Used to execute interactable events through unity or scriptable object
    /// ScriptableObjects are used for global events
    /// UnityEvents are used for local events
    /// </summary>
    public class InteractableEvent : MonoBehaviour, IInteractable
    {
        public ScriptableEvent EventScriptable;
        public UnityEvent UnityEvent;
        
        public bool Interact(GameInteractor interactor)
        {
            if(UnityEvent != null) UnityEvent?.Invoke();
            if(EventScriptable != null) EventScriptable.Invoke();
            
            return false;
        }

        public Vector3 GetWorldCenter()
        {
            return this.transform.position;
        }
    }
}