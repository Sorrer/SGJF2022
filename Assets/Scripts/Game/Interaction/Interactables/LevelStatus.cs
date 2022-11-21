using UnityEngine;
using System;
using Game.Common.Interactable.Interactables;
using LvlStat = Game.Interaction.Interactables.LevelTracker.LvlStat;

public class LevelStatus : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Color colorClosed;
    public Color colorOpen;

    public Color hoverColor;
    
    [SerializeField]
    public LevelStatusSO status;

    public void Start()
    {
        SetStatus(this.status);
    }

    public void SetStatus(LevelStatusSO newStatus)
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
