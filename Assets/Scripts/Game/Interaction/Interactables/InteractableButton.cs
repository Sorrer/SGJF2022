using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    SpriteRenderer renderer;
    Color baseColor;
    public LevelTracker tracker;

    [SerializeField] float hoverOffset, clickOffset; // color offset for mouse action
    [SerializeField] int buttonNum; // Unique to each button (eg. 1, 2, 3), don't change

    bool clicked;

    void Start() {
        renderer = GetComponent<SpriteRenderer>();
        baseColor = renderer.color;
    }

    void OnMouseOver() {
        if (Input.GetMouseButton(0)) {
            renderer.color = new Color(baseColor.r-clickOffset, baseColor.g-clickOffset,
                                    baseColor.b-clickOffset, baseColor.a);
            clicked = true;
        } else {
            renderer.color = new Color(baseColor.r-hoverOffset, baseColor.g-hoverOffset,
                                    baseColor.b-hoverOffset, baseColor.a);
        }
        if (Input.GetMouseButtonUp(0) && clicked) {
            OpenLevel();
        }
    }

    void OnMouseExit() {
        renderer.color = baseColor;
    }

    void OpenLevel() {
        Debug.Log("Open level " + tracker.levels[buttonNum-1].levelNum);
    }
}
