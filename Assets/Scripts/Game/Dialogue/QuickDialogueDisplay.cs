using System.Collections;
using System.Data.Common;
using UnityEngine;

namespace Game.Dialogue
{
    public class QuickDialogueDisplay : MonoBehaviour
    {

        private GameObject currentTextBox;
        
        public Transform TextBoxRootLocation;

        public Animation animator;
        
        
        public void Display(QuickDialogueData.ConditionalDialogue data, TextBoxGroup group)
        {
            if (currentTextBox != null)
            {
                Destroy(currentTextBox);
            }
            
            // Bring up UI (remove previous text and stop previous coroutine if exists)
            // Play animation if there is one
            if (data.animation)
            {
                animator.Stop();
                animator.clip = data.animation;
                animator.Play();
            }

            var pair = group.GetType(data.typerSettings ? data.typerSettings.type : TextBoxGroupType.Normal);
            // Spawn TextBoxPrefab at current screen location + offset (TextBoxRootLocation)
            currentTextBox = Instantiate(pair.typerPrefab);
            currentTextBox.transform.position = TextBoxRootLocation.position;
            currentTextBox.transform.SetParent(TextBoxRootLocation, true);
            
            // Type/display the text

            var typer = currentTextBox.GetComponent<TextBoxTyper>();
            
            typer.Type(data.dialogue, data.typerSettings == null ? pair.defaultSettings : data.typerSettings);
        }

    }
}