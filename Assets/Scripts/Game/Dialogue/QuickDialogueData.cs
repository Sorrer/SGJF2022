using System;
using System.Collections.Generic;
using Game.Common.ScriptableData;
using Game.Conditions;
using UnityEngine;

namespace Game.Dialogue
{
    [CreateAssetMenu(fileName = "Quick Dialogue Data",menuName = "Data/Quick Dialogue")]
    public class QuickDialogueData : ScriptableObject
    {
        [Serializable]
        public class ConditionalDialogue
        {
            public bool WhenFalse; // Activates this dialogue when false
            public ConditionBase condition;
            public string dialogue;
            public AnimationClip animation;
            public TextBoxTyperSettings typerSettings;
            
            [HideInInspector] public int _executionAmounts; // To create an even amount of executions for this conditional dialogue
        }

        public bool EvenDistrubute = true;
        
        public List<ConditionalDialogue> dialogue = new List<ConditionalDialogue>();

        public List<ConditionalDialogue> GetTrue()
        {
            List<ConditionalDialogue> trueDialogues = new List<ConditionalDialogue>();
            foreach (var d in dialogue)
            {
                if (d.condition == null)
                {
                    trueDialogues.Add(d);
                    continue;
                }

                var isTrue = d.condition.IsTrue();
                
                if (isTrue || (!isTrue && d.WhenFalse))
                {
                    trueDialogues.Add(d);
                }
            }

            return trueDialogues;
        }
        
    }
}