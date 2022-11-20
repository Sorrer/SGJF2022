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

    public bool Opened;
    public bool Locked;
    
    public bool PlaySound;
    public SoundEmitter FailedEnterDoorSound;
    public SoundEmitter EnterDoorSound;

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
            if (ConditionBase.IsTrueAll(enableWhen))
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
        if (PlaySound)
        {
            
        }
        
        if (forceLevel.Equals(""))
        {
            // TODO: Get next level
        }
        else
        {
            LevelLoader.LoadLevel(forceLevel);
        }
        
        
    }
    public void Open()
    {
        if(OnOpen != null) OnOpen?.Invoke();
    }
    public void ForceOpen()
    {
        if(OnForcedOpen != null) OnForcedOpen?.Invoke();
    }

    
    public void Close()
    {
        if(OnClosed != null) OnClosed?.Invoke();
    }
    public void ForceClose()
    {
        if(OnForcedClosed != null) OnForcedClosed?.Invoke();
    }

}
