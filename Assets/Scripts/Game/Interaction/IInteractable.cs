using UnityEngine;

namespace Game.Common.Interactable
{
    public interface IInteractable
    {
        public bool Interact(GameInteractor interactor);

        public Vector3 GetWorldCenter();
    }
}