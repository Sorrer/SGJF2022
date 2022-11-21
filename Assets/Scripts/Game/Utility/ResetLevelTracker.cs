using System.Collections;
using System.Collections.Generic;
using Game.Common.Interactable.Interactables;
using Game.Interaction.Interactables;
using UnityEngine;

public class ResetLevelTracker : MonoBehaviour
{
    public LevelTracker tracker;

    public List<LevelStatusSO> levels = new List<LevelStatusSO>();
    // Start is called before the first frame update
    void Awake()
    {
        tracker.ClearLevels();

        foreach (var level in levels)
        {
            if (level.levelNum == 1) level.isOpen = true;
            else
                level.isOpen = false;
            
            LevelTracker.levels.Add(level);
        }
    }

}
