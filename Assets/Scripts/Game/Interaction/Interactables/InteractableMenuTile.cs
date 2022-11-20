using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMenuTile : MonoBehaviour
{
        Rigidbody2D target; // Object being dragged
        Vector3 offset, mouseLoc;

        private float initialX, initialY;

        GameObject gameTarget, gameDestination;
        public List<LevelStatus> levelTiles = new List<LevelStatus>();
        private List<Vector3> initialPosition = new List<Vector3>();

        Vector3 initialVector;

        bool insertBefore; // true if insert before, false after

        public LevelTracker tracker;

        void Start() {
            // TODO: Remove this
            tracker.levels.Clear();
            
            if (tracker.levels.Count == 0) { // If empty, add in levels

                foreach(var tile in levelTiles)
                {
                    tracker.levels.Add(tile.status);
                    initialPosition.Add(tile.transform.position);
                }
            }
        }

        void Update() {
            mouseLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0)) {
                Collider2D targetCollider = Physics2D.OverlapPoint(mouseLoc);
                if (targetCollider) {
                    gameTarget = targetCollider.transform.gameObject;
                    target = gameTarget.GetComponent<Rigidbody2D>();
                    initialVector = targetCollider.transform.position;
                    offset = target.transform.position - mouseLoc;
                }
            }

            if (Input.GetMouseButtonUp(0) && target) {
                // When release, if colliding swap, else return to initial
                target.velocity = Vector2.zero;
                var trigger = target.GetComponent<TriggerTracker>();
                if (trigger.isColliding) {
                    gameDestination = trigger.colliding[0];

                    float closestPosition = float.PositiveInfinity;

                    foreach(var go in trigger.colliding)
                    {
                        float dist = Vector3.Distance(go.transform.position, target.transform.position);

                        if(dist < closestPosition)
                        {
                            gameDestination = go;
                            closestPosition = dist;
                        }
                    }
                    // (targ, dest)
                    float targ = gameTarget.transform.position.x;
                    float dest = gameDestination.transform.position.x;
                    if (gameTarget.transform.position.x < gameDestination.transform.position.x) {
                        insertBefore = true;
                    } else {
                        insertBefore = false;
                    }
                    Debug.Log(targ + "   " + dest + "   " + insertBefore);
                    tracker.MoveLevel(gameTarget.GetComponent<LevelStatus>().status,
                                    gameDestination.GetComponent<LevelStatus>().status, insertBefore);

                    UpdateTilePositions();
                } else {
                    target.transform.position = initialVector;
                }
                target = null;
            }
        }

        public void UpdateTilePositions()
        {
            for(int i = 0; i < initialPosition.Count; i++)
            {
                GetLevelNumObj(tracker.levels[i].levelNum).transform.position = initialPosition[i];
            }   
        }

        public LevelStatus GetLevelNumObj(int levelNum)
        {
            foreach(var i in levelTiles)
            {
                if(i.status.levelNum == levelNum) return i;
            }

            return null;
        }

        void FixedUpdate() {
            if (target) {
                target.MovePosition(mouseLoc + offset);
            }
        }
}
