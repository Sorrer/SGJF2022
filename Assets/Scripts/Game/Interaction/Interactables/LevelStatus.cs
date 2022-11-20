using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelStatus : MonoBehaviour
{
    [SerializeField]
    public LvlStat status;
}

[Serializable]
public struct LvlStat {
    public int levelNum;
    public bool isOpen;
    public String sceneName;
}
