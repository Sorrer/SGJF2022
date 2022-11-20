using System;
using System.Collections;
using System.Collections.Generic;
using Game.Conditions;
using Game.Sounds;
using Game.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DoorObject : MonoBehaviour
{
    public List<ConditionBase> enableWhen = new List<ConditionBase>();

    public UnityEvent OnOpen;
    public UnityEvent OnForcedOpen;
    public UnityEvent OnClosed;
    public UnityEvent OnForcedClosed;
    public UnityEvent OnHighlight;
    public UnityEvent OnUnHighlight;

    public bool Opened;
    public bool Locked;
    
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
    
    private void Start()
    {
        if (Locked) Opened = false;

        if (Opened)
        {
            ForceOpen();
        }
        else
        {
            if (enableWhen.Count > 0 && ConditionBase.IsTrueAll(enableWhen))
            {
                ForceOpen();
            }
            else
            {
                ForceClose();
            }
        }
        
    }

    public void Enter()
    {
        var nowOpen = ConditionBase.IsTrueAll(enableWhen);
        
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
        
        if (nowOpen)
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
