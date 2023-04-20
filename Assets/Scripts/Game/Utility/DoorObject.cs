using System;
using System.Collections;
using System.Collections.Generic;
using Game.Conditions;
using Game.Interaction.Interactables;
using Game.Sounds;
using Game.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DoorObject : MonoBehaviour
{
    public bool WhenFalse;
    public bool UseOr;
    public List<ConditionBase> enableWhen = new List<ConditionBase>();

    public UnityEvent OnOpen;
    public UnityEvent OnForcedOpen;
    public UnityEvent OnClosed;
    public UnityEvent OnForcedClosed;
    public UnityEvent OnHighlight;
    public UnityEvent OnUnHighlight;

    public bool Opened;
    public bool Locked;
    public bool OpenOnStartIfConditions;
    public bool PlaySound;
    public SoundEmitter FailedEnterDoorSound;
    public SoundEmitter EnterDoorSound;
    public SoundEmitter UnlockSound;

    public LevelTracker tracker;
    
    public int currentLevel = 1; // Temp for fast coding
    
    /// <summary>
    /// Forces the door to open this level.
    /// Scene name for which this door should go to
    /// </summary>
    public string forceLevel = "";

    private bool GetState()
    {
        if (enableWhen.Count == 0) return false;
        
        var isTrue = ConditionBase.IsTrueAll(enableWhen, UseOr);
        
        Debug.Log(isTrue);
        if (WhenFalse)
        {
            isTrue = !isTrue;
        }
        
        Debug.Log("Wut" + isTrue);
        
        return isTrue;
    }
    private void Start()
    {
        if (Locked) Opened = false;

        if (Opened)
        {
            ForceOpen();
        }
        else
        {
            if (OpenOnStartIfConditions && enableWhen.Count > 0 && GetState())
            {
                ForceOpen();
            }
            else
            {
                ForceClose();
            }
        }
        
        var lvl = tracker.GetNextLevel(currentLevel);

        if (forceLevel == "" && lvl == null)
        {
            ForceClose();
        }
    }

    public void Enter()
    {
        var nowOpen = GetState();
        
        if (PlaySound)
        {
            if (Opened)
            {
                EnterDoorSound.PlaySound();
            }
            else if(nowOpen)
            {
                UnlockSound.PlaySound();
            }
            else
            {
                FailedEnterDoorSound.PlaySound();
            }
        }

        if (Locked) return;
        
        if (nowOpen && !Opened)
        {
            Open();

            return;
        }

        if (!Opened) return;
        
        if (forceLevel.Equals(""))
        {
            var lvl = tracker.GetNextLevel(currentLevel);
            LevelLoader.LoadLevel(lvl.sceneName);
        }
        else
        {
            LevelLoader.LoadLevel(forceLevel);
        }
        
        
    }
    public void Open()
    {
        Opened = true;
        if(OnOpen != null) OnOpen?.Invoke();
    }
    public void ForceOpen()
    {
        Opened = true;
        if(OnForcedOpen != null) OnForcedOpen?.Invoke();
    }

    
    public void Close()
    {
        Opened = false;
        if(OnClosed != null) OnClosed?.Invoke();
    }
    public void ForceClose()
    {
        Opened = false;
        if(OnForcedClosed != null) OnForcedClosed?.Invoke();
    }


    public void Highlight()
    {
        if(OnHighlight != null) OnHighlight?.Invoke();
    }

    public void UnHighlight()
    {
        if(OnUnHighlight != null) OnUnHighlight?.Invoke();
    }
}
