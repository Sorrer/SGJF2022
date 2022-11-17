using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTracker : MonoBehaviour
{
    public List<GameObject> colliding = new List<GameObject>();
    public bool isColliding = false; // Could just checkif colliding.size > 0, but this is cached and faster
    public bool onlyCollideSolid = true;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger && onlyCollideSolid) return;
        
        colliding.Add(col.gameObject);
        
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger && onlyCollideSolid) return;
        
        colliding.Remove(other.gameObject);

        if (colliding.Count == 0) isColliding = false;
    }
}
