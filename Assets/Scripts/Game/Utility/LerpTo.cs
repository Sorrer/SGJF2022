using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTo : MonoBehaviour
{
    public Transform LerpToTransform;

    public float LerpAmount;
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position =
            Vector3.Lerp(this.transform.position, LerpToTransform.position, Time.deltaTime * LerpAmount);
    }
}
