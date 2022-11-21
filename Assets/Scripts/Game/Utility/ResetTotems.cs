using System;
using System.Collections;
using System.Collections.Generic;
using Game.Common.ScriptableData.Values;
using UnityEngine;

public class ResetTotems : MonoBehaviour
{
    public List<BoolScriptableData> totemCheck = new List<BoolScriptableData>();
    public IntScriptableData totemTotal;
    private void Awake()
    {
        foreach (var t in totemCheck)
        {
            t.value = false;
        }

        totemTotal.value = 0;

    }
}
