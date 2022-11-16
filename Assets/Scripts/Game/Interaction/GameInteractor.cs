using System;
using System.Collections.Generic;
using UnityEngine;



namespace Game.Common.Interactable
{
    public class GameInteractor : MonoBehaviour
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        [Serializable]
        public struct InteractorData
        {
            public GameObject GameObj;
            public IInteractable Interactable;
            public IInteractableHighlight Highlight;

            public bool IsValid()
            {
                return Interactable != null;
            }
        }

        private Dictionary<GameObject, InteractorData> _nearbyInteractableDict = new();
        public InteractorData _nearestInteractable = new InteractorData();

        private void Update()
        {
            UpdateNearestInteractable();
            //Debug.Log("[" + String.Join(',', _nearbyInteractableDict.Select(x => x.Key.transform.parent.gameObject.name + ": " + x.Value.GameObj.name)) + "]");
        }

        public bool Interact()
        {
            if (!_nearestInteractable.IsValid()) return false;

            return _nearestInteractable.Interactable.Interact(this);
        }

        private void ProcessInteractable(GameObject interactableObject)
        {
            var interactable = GetInteractable(interactableObject);

            if (!interactable.IsValid() || _nearbyInteractableDict.ContainsKey(interactableObject)) return;

            _nearbyInteractableDict.Add(interactableObject, interactable);

            UpdateNearestInteractable();
        }

        private void RemoveInteractable(GameObject interactableObject)
        {
            var interactable = GetInteractable(interactableObject);

            if (!interactable.IsValid()) return;


            if (_nearestInteractable.GameObj == interactableObject)
            {
                _nearestInteractable.GameObj = null;


                if (_nearestInteractable.Highlight != null)
                {
                    _nearestInteractable.Highlight.Deactivate();
                }
            }

            _nearbyInteractableDict.Remove(interactableObject);

            UpdateNearestInteractable();
        }


        private float GetDistanceToInteractable(InteractorData data)
        {
            return GetDistanceToInteractable(data.Interactable);
        }

        private float GetDistanceToInteractable(IInteractable interactable)
        {
            return GetDistanceToObject(interactable.GetWorldCenter());
        }

        private float GetDistanceToObject(Vector3 position)
        {
            Debug.DrawLine(Vector3.positiveInfinity + new Vector3(0, 0, 1), this.transform.position + new Vector3(0, 0, 1), Color.blue, 1);
            return Mathf.Abs(Vector2.Distance(position, GetCenter()));
        }

        private Vector2 GetCenter()
        {
            return this.transform.TransformPoint(this._collider.bounds.center);
        }

        // Remove inactive interactbles
        private void CleanUpNearestInteractableDict()
        {
            List<GameObject> keysForRemoval = new List<GameObject>();
            foreach (var keyPair in _nearbyInteractableDict)
                if (keyPair.Value.GameObj == null || !keyPair.Value.GameObj.activeInHierarchy)
                    keysForRemoval.Add(keyPair.Key);
            foreach (var key in keysForRemoval)
                _nearbyInteractableDict.Remove(key);
        }

        private void UpdateNearestInteractable()
        {
            CleanUpNearestInteractableDict();

            if (_nearbyInteractableDict.Count == 0 && _nearestInteractable.GameObj != null)
            {
                _nearestInteractable.GameObj = null;
                _nearestInteractable.Interactable = null;
                _nearestInteractable.Highlight = null;
            }

            var lastInteractable = _nearestInteractable;
            _nearestInteractable = new InteractorData();


            float distance = 0;

            foreach (var interactableData in _nearbyInteractableDict.Values)
            {



                if (!_nearestInteractable.IsValid())
                {
                    _nearestInteractable = interactableData;

                    distance = GetDistanceToInteractable(interactableData);
                    continue;
                }


                float testDist = GetDistanceToInteractable(interactableData);

                if (testDist < distance)
                {
                    _nearestInteractable = interactableData;
                    distance = testDist;
                    continue;
                }
            }


            if (_nearestInteractable.GameObj != lastInteractable.GameObj && lastInteractable.IsValid())
            {
                if (lastInteractable.Highlight != null) lastInteractable.Highlight.Deactivate();
            }

            if (!_nearestInteractable.IsValid()) return;

            if (_nearestInteractable.Highlight != null) _nearestInteractable.Highlight.Activate();
        }

        private InteractorData GetInteractable(GameObject gameObject)
        {
            InteractorData data;
            data.GameObj = gameObject;
            data.Interactable = gameObject.GetComponentInParent<IInteractable>();
            data.Highlight = gameObject.GetComponentInParent<IInteractableHighlight>();


            return data;
        }






        private void OnCollisionEnter(Collision other)
        {
            ProcessInteractable(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            RemoveInteractable(other.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            ProcessInteractable(other.gameObject);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            RemoveInteractable(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            ProcessInteractable(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveInteractable(other.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ProcessInteractable(other.gameObject);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            RemoveInteractable(other.gameObject);
        }
    }
}