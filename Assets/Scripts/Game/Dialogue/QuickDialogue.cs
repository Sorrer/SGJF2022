using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dialogue
{
 
    [RequireComponent(typeof(QuickDialogueDisplay))]
    public class QuickDialogue : MonoBehaviour
    {
        public TextBoxGroup group;
        public QuickDialogueData dialogue;
        public QuickDialogueDisplay display;

        private void Start()
        {
            foreach (var d in dialogue.dialogue)
            {
                d._executionAmounts = 0;
            }
        }

        public void Activate()
        {
            // Get dialogues with either a null condition or a condition that is true (accounting for when false)
            var dialogueOptions = dialogue.GetTrue();
            
            if (dialogueOptions.Count == 0)
            {
                //Debug.LogWarning("No dialogue found for this character " + dialogue.name + " " + this.name);
                return;
            }
            
            // Grab an dialogue randomly but with an even distribution\
            QuickDialogueData.ConditionalDialogue selected = null;
            
            if (dialogue.EvenDistrubute)
            {
                int min = dialogueOptions.Min(val => val._executionAmounts);

                var lowestDialgoues = dialogueOptions.Where(val => val._executionAmounts == min).ToArray();

                selected = lowestDialgoues[Random.Range(0, lowestDialgoues.Count())];

                selected._executionAmounts += 1;
            }
            else
            {
                selected = dialogueOptions[Random.Range(0, dialogueOptions.Count())];
            }
            
            // Display the dialogue via QuickDialogueDisplay
            display.Display(selected, group);
        }
    }
}