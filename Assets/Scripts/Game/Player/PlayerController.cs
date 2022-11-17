using System;
using Game.Common.Interactable;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public GameInteractor interactor;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                interactor.Interact();
            }
        }
    }
}