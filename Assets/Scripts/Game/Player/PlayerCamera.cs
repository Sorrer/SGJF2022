using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Rigidbody2D camBody;
    [SerializeField] private int scrollSpeed;
    [SerializeField] private Transform MaxLeftTransform;
    [SerializeField] private Transform MaxRightTransform;

    public float Speed = 10;

    void Start() {
        camBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 camMovementVec = Vector2.zero;
        if (Input.GetKey(KeyCode.A) && transform.position.x > MaxLeftTransform.position.x) // move L
            {
                camMovementVec.x -= Speed;
            }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < MaxRightTransform.position.x) // move R
            {
                camMovementVec.x += Speed;
            }
        else
            {
                camMovementVec.x = 0;
            }

        camMovementVec.x *= scrollSpeed;
        camBody.velocity = camMovementVec;
    }
}
