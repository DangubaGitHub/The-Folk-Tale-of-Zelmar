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

    bool isRunning;

    Animator playerAnim;
    SpriteRenderer playerSr;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSr = GetComponent<SpriteRenderer>();
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

        if (Input.GetButton("Run") && walkSpeed == 6f)
        {
            isRunning = true;
            if (isRunning == true)
            {
                Invoke("Run", .5f);
            }
            if(playerRb.velocity.x == 0)
            {
                isRunning = false;
            }
        }
        else
            walkSpeed = 6f;

        if (playerRb.velocity.x < 0)
        {
            playerSr.flipX = true;
            //transform.localScale = new Vector2(-1, 1);
        }
        else if (playerRb.velocity.x > 0)
        {
            playerSr.flipX = false;
            //transform.localScale = new Vector2(1, 1);
        }
    }


    void Run()
    {
        walkSpeed = 12f;
    }
    /*void Walking()
    {
        playerRb.velocity = new Vector2(walkSpeed * Input.GetAxis("Horizontal"), playerRb.velocity.y);

        if(playerRb.velocity.x > 0)
        {
            playerAnim.SetBool("")
        }
    }*/
}
