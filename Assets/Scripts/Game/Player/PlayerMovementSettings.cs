using System;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(fileName = "Player Movement Settings", menuName = "Settings/PlayerMovement")]
    public class PlayerMovementSettings : ScriptableObject
    {
        public float JumpForce;
        public float Speed;
        public float gravity;
        public float maxVerticalSpeed;
        public float coyoteTime;
        
        public float groundingForce;

        [Space(4)] [Header("Animation Settings")]
        public AnimationCurve SpeedToAnimationSpeedCurve;
        public float speedToAnimationMultiplier;

    }
}