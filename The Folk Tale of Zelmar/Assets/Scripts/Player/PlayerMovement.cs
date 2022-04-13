using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRb;

    [Header("Movement Attributes")] 
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;

    bool isGrounded;
    [SerializeField] Transform groundPoint;
    [SerializeField] LayerMask whatIsGround;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        playerRb.velocity = new Vector2(walkSpeed * Input.GetAxis("Horizontal"), playerRb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            }
        }
    }
}
