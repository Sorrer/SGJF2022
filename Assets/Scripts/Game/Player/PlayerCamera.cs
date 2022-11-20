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
    public int padding = 100;

    public GameObject rightSide;
    public GameObject leftSide;

    void Start() {
        camBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 camMovementVec = Vector2.zero;
        
        
        leftSide.SetActive(false);
        rightSide.SetActive(false);
        
        if (((Input.mousePosition.x < 0 + padding) || Input.GetKey(KeyCode.A)) && transform.position.x > MaxLeftTransform.position.x) // move L
            {
                leftSide.SetActive(true);
                camMovementVec.x -= Speed;
            }
        else if ((Input.mousePosition.x > Screen.width - padding || Input.GetKey(KeyCode.D)) && transform.position.x < MaxRightTransform.position.x) // move R
            {
                rightSide.SetActive(true);
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
