using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Rigidbody2D camBody;
    [SerializeField] private int scrollSpeed;
    [SerializeField] private float maxRight;
    private float maxLeft;

    void Start() {
        camBody = GetComponent<Rigidbody2D>();
        maxLeft = transform.position.x;
    }

    void Update()
    {
        Vector2 camMovementVec = Vector2.zero;
        if (Input.GetKey(KeyCode.A) && transform.position.x > maxLeft) // move L
            {
                camMovementVec.x -= 1;
            }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < maxRight) // move R
            {
                camMovementVec.x += 1;
            }
        else
            {
                camMovementVec.x = 0;
            }

        camMovementVec.Normalize();
        camMovementVec.x *= scrollSpeed;
        camBody.velocity = camMovementVec;
    }
}
