using System.Collections;
using BrunoMikoski.TextJuicer;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Game.Dialogue
{
   

    public enum TextBoxGroupType
    {
        Normal, Angry, Crazy // Create these 
    }

    
    public class TextBoxTyper : MonoBehaviour
    {
        public bool reverseAnimationAfterFinish; // Make the animation go away
        public float pauseAtProgress = -1; // Pause at a time instead of using reverse animation Uses this first instead of reverse
        
        public TextMeshPro textMesh;
        public TMP_TextJuicer juicer;

        private Coroutine currentCoroutine = null;
        
        private float currentTime;
        
        public void Type(string text, TextBoxTyperSettings settings)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            
            currentCoroutine = StartCoroutine(PlayJuicer(text, settings));
        }

        public IEnumerator PlayJuicer(string text, TextBoxTyperSettings settings)
        {

            //textMesh.text = text;
            juicer.Text = text;
            juicer.SetProgress(0);
            currentTime = 0;
            float progress = 0;
            float maxDuration = settings.duration;

            if (settings.durationPerCharacter)
            {
                maxDuration *= text.Length;
            }

            if (pauseAtProgress > 0)
            {
                while (progress < pauseAtProgress)
                {
                    Debug.Log(progress);
                    progress = currentTime / maxDuration;
                
                    juicer.SetProgress(progress);
                
                    currentTime += Time.deltaTime;
                
                    yield return null;
                }
                
                yield return new WaitForSeconds(settings.waitForDuration);
                
                while (currentTime <= maxDuration)
                {
                    progress = currentTime / maxDuration;
                
                    juicer.SetProgress(progress);
                
                    currentTime += Time.deltaTime;
                
                    yield return null;
                }
            }
            else
            {
                while (currentTime <= maxDuration)
                {
                    progress = currentTime / maxDuration;
                
                    juicer.SetProgress(progress);
                
                    currentTime += Time.deltaTime;
                
                    yield return null;
                }
                
                yield return new WaitForSeconds(settings.waitForDuration);


                if (reverseAnimationAfterFinish)
                {
                    while (currentTime > 0)
                    {
                        progress = currentTime / maxDuration;
                
                        juicer.SetProgress(progress);
                
                        currentTime -= Time.deltaTime;
                
                        yield return null;
                    }
                }
            }
            
            
            Destroy(this.gameObject);
        }
    }
}