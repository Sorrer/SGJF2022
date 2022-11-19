using Game.Sounds;
using UnityEngine;

namespace Game.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        public SoundEmitter walking;
        public SoundEmitter Jump;
        public SoundEmitter Land;
        public SoundEmitter LandSmall;
        public PlayerMovement movement;
    
        public void PlaySoundStep()
        {
            if (movement.isGroundedTracker.isColliding)
            {
                walking.PlaySound();
            }
        }

        public void PlayJumpSound()
        {
            Jump.PlaySound();
        }

        public void PlayLand()
        {
            Land.PlaySound();
        }

        public void PlayLandSmall()
        {
            LandSmall.PlaySound();
        }

    }
}
