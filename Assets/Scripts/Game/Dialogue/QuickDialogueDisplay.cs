using System.Collections;
using UnityEngine;

namespace Game.Dialogue
{
    public class QuickDialogueDisplay : MonoBehaviour
    {
        public float DisplayTime;
        public GameObject TextBoxPrefab;
        
        public Transform TextBoxRootLocation;
        
        private Coroutine current;
        public void Display(string text)
        {
            if (current != null)
            {
                StopCoroutine(current);
            }
            
            current = StartCoroutine(DisplayCoroutine(text));
        }


        public IEnumerator DisplayCoroutine(string text)
        {
            
            // Bring up UI (remove previous text and stop previous coroutine if exists
            
            // Spawn TextBoxPrefab at current screen location + offset (TextBoxRootLocation)
            
            
            // Type/display the text
            
            // Wait
            yield return new WaitForSeconds(DisplayTime);

            // Bring down UI

            current = null;

        }
    }
}