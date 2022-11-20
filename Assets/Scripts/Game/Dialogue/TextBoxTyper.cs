using System;
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
        
        private float currentTime, currentPauseTime;

        private bool Playing, reversing;
        private float progress, maxDuration, waitTime;

        private void Awake()
        {
            juicer.SetProgress(0);
        }

        public void Type(string text, TextBoxTyperSettings settings)
        {
            if (currentCoroutine != null)
            {
                Playing = false;
            }
            
            juicer.Text = text;
            juicer.SetProgress(0);
            Playing = true;
            progress = 0;
            
            // Used to be in a coroutine, but tmp does not like that
            maxDuration = settings.duration;

            if (settings.durationPerCharacter)
            {
                maxDuration *= text.Length;
            }

            waitTime = settings.waitForDuration;
            
            currentTime = 0;
            currentPauseTime = 0;
            reversing = false;
        }

        private void Update()
        {

            if (Playing)
            {

                if (pauseAtProgress > 0)
                {
                    if (progress <= pauseAtProgress)
                    {
                        progress = currentTime / maxDuration;
                        juicer.SetProgress(progress);
                        currentTime += Time.deltaTime;
                    }
                    else if (waitTime < 0 || currentPauseTime <= waitTime)
                    {
                        if (waitTime > 0) // This dialogue is infinite
                        {
                            currentPauseTime += Time.deltaTime;
                        }
                    }
                    else
                    {
                        progress = currentTime / maxDuration;
                        juicer.SetProgress(progress);
                        currentTime += Time.deltaTime;

                        if (progress > 1)
                        {
                            Playing = false;
                            Destroy(this.gameObject);
                        }
                    }
                    
                }
                else
                {
                    if (progress <= 1)
                    {
                        currentTime += Time.deltaTime;
                        progress = currentTime / maxDuration;
                        juicer.SetProgress(progress);
                    }
                    else if (reverseAnimationAfterFinish && progress >= 0)
                    {
                        currentTime -= Time.deltaTime;
                        progress = currentTime / maxDuration;
                        juicer.SetProgress(progress);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }

        public IEnumerator PlayJuicer(string text, TextBoxTyperSettings settings)
        {

            //textMesh.text = text;
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
                    currentTime += Time.deltaTime;
                    
                    progress = currentTime / maxDuration;
                
                    juicer.SetProgress(progress);
                
                
                    yield return new WaitForEndOfFrame();
                }
                
                yield return new WaitForSeconds(settings.waitForDuration);
                
                while (currentTime <= maxDuration)
                {
                    currentTime += Time.deltaTime;
                    
                    progress = currentTime / maxDuration;
                
                    juicer.SetProgress(progress);
                
                
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                while (currentTime <= maxDuration)
                {
                    currentTime += Time.deltaTime;
                    
                    progress = currentTime / maxDuration;
                
                    juicer.SetProgress(progress);
                
                
                    yield return new WaitForEndOfFrame();
                }
                
                yield return new WaitForSeconds(settings.waitForDuration);


                if (reverseAnimationAfterFinish)
                {
                    while (currentTime > 0)
                    {
                        currentTime -= Time.deltaTime;
                        
                        progress = currentTime / maxDuration;
                
                        juicer.SetProgress(progress);
                
                
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
            
            
            Destroy(this.gameObject);
        }
    }
}