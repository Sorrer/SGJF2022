using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelStatus : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Color colorClosed;
    public Color colorOpen;
    
    [SerializeField]
    public LvlStat status;

    public void SetStatus(LvlStat newStatus)
    {
        status = newStatus;

        if (status.isOpen)
        {
            renderer.color = colorOpen;
        }
        else
        {
            renderer.color = colorClosed;
        }
    }
}

[Serializable]
public struct LvlStat {
    public int levelNum;
    public bool isOpen;
    public String sceneName;
}
