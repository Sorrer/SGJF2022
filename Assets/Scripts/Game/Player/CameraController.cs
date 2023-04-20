using System;
using UnityEngine;

namespace Game.Player
{
    public class CameraController : MonoBehaviour
    {

        public GameObject target;

        public bool UseFixedUpdate = true;
        public float snappingDistance;
        public float smoothingAmount;
        public float smoothingDistance;
        public AnimationCurve SmoothingMultiplierCurve;
        
        public void SetTarget(GameObject go)
        {
            this.target = go;
        }


        public void Update()
        {
            if(!UseFixedUpdate) UpdateCamera();
        }

        public void FixedUpdate()
        {
            if(UseFixedUpdate) UpdateCamera();
        }

        private void UpdateCamera()
        {
            Vector2 targetPos = new Vector2(this.target.transform.position.x, this.target.transform.position.y);
            Vector2 currentPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 diffPos = targetPos - currentPos;
            diffPos.Normalize();
            
            float distance = Vector2.Distance(targetPos, currentPos);
    

            if (distance > snappingDistance)
            {
                this.transform.position = diffPos * snappingDistance;
            }
            else
            {
                float smoothDist = Mathf.Clamp(distance, 0, smoothingDistance);

                float smoothMulti = SmoothingMultiplierCurve.Evaluate(smoothDist / smoothingDistance);

                Vector2 moveAmount = diffPos * (smoothMulti * smoothingAmount * distance);

                this.transform.position = this.transform.position + new Vector3(moveAmount.x, moveAmount.y);
                
                Debug.Log(moveAmount + " " + diffPos + " " + smoothMulti + " " + smoothingAmount + " " + distance);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(this.transform.position, new Vector3(this.smoothingDistance, this.smoothingDistance, 1));
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(this.transform.position, new Vector3(this.snappingDistance, this.snappingDistance, 1));
        }
    }
}