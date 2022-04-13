using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D playerRb;
	
	public float moveSpeed;

	public float sprintMultiplier; 
	public float sprintDelay;      
	private float sprintTimer;      
	private bool jumpedDuringSprint; 

	public float initialJumpForce;      
	public float extraJumpForce;        
	public float maxExtraJumpTime;      
	public float delayToExtraJumpForce; 
	private float jumpTimer;             
	private bool playerJumped;         
	private bool playerJumping;         
     
	private bool isGrounded;
	SpriteRenderer playerSr;
	Animator playerAnim;

	[SerializeField] Transform groundPoint;
	[SerializeField] LayerMask whatIsGround;

	private void Awake()
    {
		playerRb = GetComponent<Rigidbody2D>();
		playerSr = GetComponent<SpriteRenderer>();
		playerAnim = GetComponent<Animator>();
    }

    void Update()
	{
		isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			playerJumped = true; 
			playerJumping = true; 
			jumpTimer = Time.time;
		}

		if (Input.GetButtonUp("Jump") || Time.time - jumpTimer > maxExtraJumpTime)
		{
			playerJumping = false;
		}

		if (Input.GetButtonDown("Horizontal"))
		{
			sprintTimer = Time.time;
			jumpedDuringSprint = false;

			playerAnim.SetBool("isWalking", true);
			playerAnim.SetBool("isRunning", false);
		}
		else
		{
			playerAnim.SetBool("isRunning", false);
			playerAnim.SetBool("isWalking", false);
		}

		if (playerRb.velocity.x <0)
        {
			playerSr.flipX = true;
        }
		else if(playerRb.velocity.x > 0)
        {
			playerSr.flipX = false;
        }

		SetAnimations();
	}

	void FixedUpdate()
	{
		if (Input.GetButton("Run") && Time.time - sprintTimer > sprintDelay && isGrounded || jumpedDuringSprint)
		{
			playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * sprintMultiplier, playerRb.velocity.y);

		
				playerAnim.SetBool("isWalking", false);
				playerAnim.SetBool("isRunning", true);

				playerAnim.SetBool("isJumping", false);
				playerAnim.SetBool("isFaling", false);


			if (playerJumped)
			{
				jumpedDuringSprint = true; 
			}
		}
		else
		{
			playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, playerRb.velocity.y);

		}

		if (playerJumped)
		{
			playerRb.AddForce(new Vector2(0, initialJumpForce));
			playerJumped = false;
		}

		if (playerJumping && Time.time - jumpTimer > delayToExtraJumpForce)
		{
			playerRb.AddForce(new Vector2(0, extraJumpForce));
		}
	}

	void SetAnimations()
    {
		if(playerRb.velocity.y == 0 && isGrounded)
        {
			playerAnim.SetBool("isJumping", false);
			playerAnim.SetBool("isFaling", false);
		}

		if(playerRb.velocity.y > 0f && !isGrounded)
        {
			playerAnim.SetBool("isJumping", true);
        }

		if (playerRb.velocity.y < 0f && !isGrounded)
		{
			playerAnim.SetBool("isJumping", false);
			playerAnim.SetBool("isFaling", true);
		}
		else
			isGrounded = true;
		
    }
}
