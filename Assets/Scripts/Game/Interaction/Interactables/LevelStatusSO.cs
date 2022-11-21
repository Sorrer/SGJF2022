using System;
using UnityEngine;

namespace Game.Common.Interactable.Interactables
{
    [CreateAssetMenu(fileName = "Level Status", menuName = "Data/Level Status")]
    public class LevelStatusSO : ScriptableObject
    {
        public int levelNum;
        public bool isOpen;
        public String sceneName;
    }
}