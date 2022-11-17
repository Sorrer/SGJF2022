using System.Collections;
using System.Collections.Generic;
using Game.Player;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerMovementSettings animationSettings;

    public Animator animator;

    public SpriteRenderer RatSprite;
    
    private static readonly int IS_FALLING = Animator.StringToHash("IsFalling");
    private static readonly int IS_RISING = Animator.StringToHash("IsRising");
    private static readonly int SPEED = Animator.StringToHash("Speed");
    private static readonly int STOP_STAND = Animator.StringToHash("StopStand");
    private static readonly int STAND = Animator.StringToHash("Stand");
    private static readonly int JUMP = Animator.StringToHash("Jump");

    public void PlayJump()
    {
        animator.SetTrigger(JUMP);
    }

    public void PlayStand()
    {
        animator.ResetTrigger(STOP_STAND);
        animator.SetTrigger(STAND);
    }

    public void StopStand()
    {
        animator.SetTrigger(STOP_STAND);
    }

    public void SetDirection(int dir)
    {
        if (dir > 0)
        {
            RatSprite.flipX = false;
        }
        else
        {
            RatSprite.flipX = true;
        }
    }
    
    public void SetSpeed(float val, float max)
    {
        float newVal = animationSettings.speedToAnimationMultiplier *
                       animationSettings.SpeedToAnimationSpeedCurve.Evaluate(val / max);
        
        animator.SetFloat(SPEED, newVal);
    }

    public void SetVerticalVelocity(float val)
    {
        if (val > 0)
        {
            animator.SetBool(IS_FALLING, false);
            animator.SetBool(IS_RISING, true);
        }
        else
        {
            animator.SetBool(IS_FALLING, true);
            animator.SetBool(IS_RISING, false);
        }
    }
    
    


}
