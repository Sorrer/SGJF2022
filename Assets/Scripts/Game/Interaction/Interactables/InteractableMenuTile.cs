using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Game.Interaction.Interactables;
using UnityEngine;
using LvlStat = Game.Interaction.Interactables.LevelTracker.LvlStat;

public class InteractableMenuTile : MonoBehaviour
{
        Rigidbody2D target; // Object being dragged
        Vector3 offset, mouseLoc;

        private float initialX, initialY;

        GameObject gameTarget, gameDestination;
        public List<LevelStatus> levelTiles = new List<LevelStatus>();
        public List<InteractableButton> levelButtons = new List<InteractableButton>();
        private List<Vector3> initialPosition = new List<Vector3>();

        Vector3 initialVector;

        bool insertBefore; // true if insert before, false after

        public LevelTracker tracker;

        [SerializeField] float shiftOffset;

        void Awake() {
            if (LevelTracker.levels.Count == 0) { // If empty, add in levels
                Debug.Log("Building levels");
                foreach(var tile in levelTiles)
                {
                    LevelTracker.levels.Add(tile.status);
                    initialPosition.Add(tile.transform.position);
                }
            }
            else
            {
                Debug.Log("Setting level tiles");
                
                tracker.print();
                
                
                foreach (LevelStatus tile in levelTiles)
                {
                    initialPosition.Add(tile.transform.position);
                    tile.SetStatus(tracker.GetLevel(tile.status.levelNum));
                    Debug.Log("Tile being set " + tile.status.levelNum + " to " + tracker.GetLevelIndexOf(tile.status.levelNum));
                    
                }
                
                UpdateTilePositions();
            }
        }

        Collider2D targetCollider;
        //int hoverShifted, hoverCurr; // so only shifts once
        Vector3 destInitialPos;
        void Update() {
            mouseLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0)) {
                targetCollider = Physics2D.OverlapPoint(mouseLoc);
                if (targetCollider != null && (gameTarget = targetCollider.transform.gameObject).tag != "Button") {
                    if (targetCollider)
                    {

                        var lvlStatus = gameTarget.GetComponent<LevelStatus>();

                        if (!lvlStatus.status.isOpen) return;
                        
                        target = gameTarget.GetComponent<Rigidbody2D>();
                        initialVector = targetCollider.transform.position;
                        offset = target.transform.position - mouseLoc;
                    }
                }
            }

            foreach (var tile in levelTiles) {
                tile.transform.GetChild(0).transform.localPosition = Vector3.zero;
            }

            if (Input.GetMouseButton(0) && targetCollider != null && gameTarget.tag != "Button") {
                // need to ignore collision w/ button?
                var targTrigger = target.GetComponent<TriggerTracker>();
                if (targTrigger.isColliding) {
                    gameDestination = targTrigger.colliding[0];
                    float closestPosition = float.PositiveInfinity;
                    foreach (var go in targTrigger.colliding) {
                        float dist = Vector3.Distance(go.transform.position, target.transform.position);
                        if (dist < closestPosition) {
                            gameDestination = go;
                            closestPosition = dist;
                        }
                    }
                    destInitialPos = initialPosition[GetLevelNumIndex(gameDestination.GetComponent<LevelStatus>().status.levelNum)];

                    // Shift hover object
                    foreach (var item in targTrigger.colliding) {
                        if (gameTarget.transform.position.x < gameDestination.transform.position.x) { //shift right
                            gameDestination.transform.GetChild(0).transform.localPosition = new Vector3(shiftOffset,0,0);
                        } else { // shift left
                            gameDestination.transform.GetChild(0).transform.localPosition = new Vector3(-shiftOffset,0,0);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && target) {
                // When release, if colliding swap, else return to initial
                target.velocity = Vector2.zero;
                var trigger = target.GetComponent<TriggerTracker>();
                
                if (trigger.isColliding) {
                    Debug.Log("i collide effectively ()b");
                    gameDestination = trigger.colliding[0];

                    var levelStatus = gameDestination.GetComponent<LevelStatus>();

                    if (!levelStatus.status.isOpen || levelStatus == null)
                    {
                        Debug.Log("Not working");
                        target.transform.position = initialVector;
                        if(gameDestination != null) gameDestination.transform.position = destInitialPos;
                        target = null;
                        return;
                    }

                    float closestPosition = float.PositiveInfinity;
                    foreach (var go in trigger.colliding) {
                        float dist = Vector3.Distance(go.transform.position, target.transform.position);

                        if (dist < closestPosition) {
                            gameDestination = go;
                            closestPosition = dist;
                        }
                    }
                    // (targ, dest)
                    if (gameTarget.transform.position.x < gameDestination.transform.position.x) {
                        insertBefore = true;
                    } else {
                        insertBefore = false;
                    }
                    tracker.MoveLevel(gameTarget.GetComponent<LevelStatus>().status,
                                    gameDestination.GetComponent<LevelStatus>().status, insertBefore);
                    
                    UpdateTilePositions();
                } else {
                    target.transform.position = initialVector;
                    if(gameDestination != null) gameDestination.transform.position = destInitialPos;
                }
                target = null;
            }
        }

        public void UpdateTilePositions()
        {
            for(int i = 0; i < initialPosition.Count; i++)
            {
                GetLevelNumObj(LevelTracker.levels[i].levelNum).transform.position = initialPosition[i];
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

        public int GetLevelNumIndex(int levelNum)
        {
            for(int i = 0; i < levelTiles.Count; i++)
            {
                if(levelTiles[i].status.levelNum == levelNum) return i;
            }

            return -1;
        }

        void FixedUpdate() {
            if (target) {
                target.MovePosition(mouseLoc + offset);
            }
        }
}
