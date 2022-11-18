using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{    
    [CreateAssetMenu(fileName = "Typer Group", menuName = "Data/Typer Group")]
    public class TextBoxGroup : ScriptableObject
    {
        [Serializable]
        public class TextPair
        {
            public TextBoxTyperSettings defaultSettings;
            public GameObject typerPrefab;
            public TextBoxGroupType type;
        }

        public List<TextPair> textPair = new List<TextPair>();

        public TextPair GetType(TextBoxGroupType type)
        {
            foreach(var pair in textPair)
            {
                if (pair.type == type)
                {
                    return pair;
                }
            }

            Debug.LogError("Could not get text group of " + type.ToString());
            
            return textPair.Count > 0 ? textPair[0] : null;
        }
    }
}