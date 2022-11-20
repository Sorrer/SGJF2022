using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class LevelStatus : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Color colorClosed;
    public Color colorOpen;

    public Color hoverColor;
    
    [SerializeField]
    public LvlStat status;

    public void Start()
    {
        SetStatus(this.status);
    }

    public void SetStatus(LvlStat newStatus)
    {
        status = newStatus;

        if (status.isOpen)
        {
            renderer.color = colorOpen;
            renderer.sortingOrder = 0;
        }
        else
        {
            renderer.color = colorClosed;
            renderer.sortingOrder = -1;
        }
    }

    private void OnMouseEnter()
    {
        if (status.isOpen)
        {
            renderer.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        SetStatus(this.status);
    }
}

[Serializable]
public struct LvlStat {
    public int levelNum;
    public bool isOpen;
    public String sceneName;
}
