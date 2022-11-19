using FMODUnity;
using UnityEngine;

namespace Game.Sounds
{
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] private EventReference audioKey = default;
    
        public void PlaySound()
        {
            RuntimeManager.PlayOneShotAttached(audioKey, this.gameObject);
        }
        public void PlaySoundAnimation(AnimationEvent e)
        {
            PlaySound();
        }
    }
}
