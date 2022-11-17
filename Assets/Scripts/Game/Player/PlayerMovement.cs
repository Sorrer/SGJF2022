using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rigidbody2D;

        public PlayerAnimation playerAnimation;
        
        private Vector2 vel, moveVel;
        
        public TriggerTracker isGroundedTracker;
        public TriggerTracker isHeadTrigger;
        public TriggerTracker isLeftSideTrigger;
        public TriggerTracker isRightSideTrigger;

        public Stopwatch coyoteTimer = new Stopwatch();

        public bool HasJumped = false;
        public bool CanJump => (isGroundedTracker.isColliding ||
                               coyoteTimer.ElapsedMilliseconds < (movementSettings.coyoteTime * 1000)) && !HasJumped;

        
        public PlayerMovementSettings movementSettings;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = this.GetComponent<Rigidbody2D>();
            Physics2D.simulationMode = SimulationMode2D.Update;
        }

        // Update is called once per frame
        void Update()
        {
            if (isGroundedTracker.isColliding)
            {
                HasJumped = false;
                coyoteTimer.Restart();
            }
            else if (coyoteTimer.ElapsedMilliseconds > movementSettings.coyoteTime * 1000)
            {
                coyoteTimer.Stop();
            }
            
            
            
            Vector2 movementVec = Vector2.zero;
            
            if (Input.GetKey(KeyCode.A) && !isLeftSideTrigger.isColliding)
            {
                movementVec += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D) && !isRightSideTrigger.isColliding)
            {
                movementVec += Vector2.right;
            }

            if (CanJump && Input.GetKeyDown(KeyCode.Space))
            {
                HasJumped = true;
                vel.y = (Vector2.up * movementSettings.JumpForce).y;
                playerAnimation.PlayJump();
            }
            
             
            
            
            movementVec.Normalize();
            movementVec *= movementSettings.Speed;

            vel += Vector2.down * movementSettings.gravity * Time.deltaTime;
            vel.x = (movementVec *  movementSettings.Speed).x; 
            
            
            /*
            if (movementVec.x == 0)
            {
                float percentage = Mathf.Clamp((vel.x / movementSettings.maxDampeningVelocityCurve), 0, 1);
                vel.x = Mathf.Lerp(vel.x, 0, movementSettings.horizontalDampeningStrength * movementSettings.horizontalDampeningStrengthCurve.Evaluate(percentage) * Time.deltaTime);
            }*/
            
            
            // Clamp to max sure rat does fly
            /*
             float maxHorizontalSpeed = movementSettings.maxHorizontalSpeed;
            vel.x = Mathf.Clamp(vel.x, -maxHorizontalSpeed, maxHorizontalSpeed);
            */
            
            float maxVerticalSpeed = movementSettings.maxVerticalSpeed;
            vel.y= Mathf.Clamp(vel.y, -maxVerticalSpeed, float.PositiveInfinity);
            
            if (isGroundedTracker.isColliding && vel.y < 0)
            {
                vel.y = movementSettings.groundingForce;
            }

            if (isHeadTrigger.isColliding)
            {
                if (vel.y > 0)
                {
                    vel.y = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                playerAnimation.PlayStand();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                playerAnimation.StopStand();
            }
            
            if(vel.x != 0) playerAnimation.SetDirection(vel.x > 0 ? 1 : -1);
            playerAnimation.SetSpeed(Mathf.Abs(vel.x), maxVerticalSpeed);
            playerAnimation.SetVerticalVelocity(isGroundedTracker.isColliding ? 0 : vel.y);
            
            rigidbody2D.velocity = vel;
        }

    }
}
