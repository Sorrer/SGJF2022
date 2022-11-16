using UnityEngine;
using UnityEngine.Events;

namespace Game.Common.Interactable.Interactables
{
    public class HightlightEvent : MonoBehaviour, IInteractableHighlight
    {

        public UnityEvent OnActivate;
        public UnityEvent OnDeactivate;

        public void Activate()
        {
            OnActivate.Invoke();
        }

        public void Deactivate()
        {
            OnDeactivate.Invoke();
        }
    }
}