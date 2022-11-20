using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMenuTile : MonoBehaviour
{
        Rigidbody2D target; // Object being dragged
        Vector3 offset, mouseLoc;

        private float initialX, initialY;

        GameObject gameTarget, gameDestination;

        Vector3 initialVector;

        public LevelTracker tracker;

        void Start() {
            if (tracker.levels.Count == 0) { // If empty, add in levels
                foreach (Transform child in transform) {
                    tracker.levels.Add(child.GetComponent<LevelStatus>().status);
                }
            }
        }

        void Update() {
            mouseLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0)) {
                Collider2D targetCollider = Physics2D.OverlapPoint(mouseLoc);
                if (targetCollider) {
                    target = targetCollider.transform.gameObject.GetComponent<Rigidbody2D>();
                    initialVector = targetCollider.transform.position;
                    offset = target.transform.position - mouseLoc;
                }
            }

            if (Input.GetMouseButtonUp(0) && target) {
                target.velocity = Vector2.zero;
                var trigger = target.GetComponent<TriggerTracker>();
                if (trigger.isColliding) {
                    GameObject item = trigger.colliding[0];
                    target.transform.position = item.transform.position;
                    item.transform.position = initialVector;
                } else {
                    target.transform.position = initialVector;
                }
                target = null;
            }
        }

        void FixedUpdate() {
            if (target) {
                target.MovePosition(mouseLoc + offset);
            }
        }
}
