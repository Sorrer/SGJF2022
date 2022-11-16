using UnityEngine;
using UnityEngine.Events;

namespace Game.Common.Interactable.Interactables
{
    public class AnimatedInteractable : MonoBehaviour, IInteractable, IInteractableHighlight
    {
        public UnityEvent<GameInteractor> OnInteract;

        [SerializeField]
        private Animator animator;
        public void Activate()
        {
            animator.SetBool("active", true);
        }

        public void Deactivate()
        {
            animator.SetBool("active", false);
        }

        public Vector3 GetWorldCenter()
        {
            return transform.position;
        }

        public bool Interact(GameInteractor interactor)
        {
            OnInteract.Invoke(interactor);
            return true;
        }
    }
}
