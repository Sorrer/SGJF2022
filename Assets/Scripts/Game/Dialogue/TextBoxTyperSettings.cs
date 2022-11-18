using UnityEngine;

namespace Game.Dialogue
{ 
    [CreateAssetMenu(fileName = "Typer Settings", menuName = "Data/Typer Settings")]
    public class TextBoxTyperSettings : ScriptableObject
    {
        public bool durationPerCharacter;
        public float duration; // seconds
        public float waitForDuration; // Time to leave the text box up
        public TextBoxGroupType type;
    }
}