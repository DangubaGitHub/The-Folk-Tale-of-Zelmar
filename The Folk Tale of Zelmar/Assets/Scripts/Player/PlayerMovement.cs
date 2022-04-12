using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;

    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Walk();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Walk()
    {
        Vector2 playerWalkVelocity = new Vector2(moveInput.x * walkSpeed, playerRb.velocity.y);
        playerRb.velocity = playerWalkVelocity;
    }
}
