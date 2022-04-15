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

	const string IDLE = "Player_Idle";
	const string WALK = "Player_Walk";
	const string RUN = "Player_Run";
	const string JUMP = "Player_Jump";
	const string FALL = "Player_Fall";

	string currentState;

	bool atMaxSpeed;

	private void Awake()
    {
		playerRb = GetComponent<Rigidbody2D>();
		playerSr = GetComponent<SpriteRenderer>();
		playerAnim = GetComponent<Animator>();

		atMaxSpeed = false;
	}

    void Update()
	{
		isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			ChangeAnimationState(JUMP);

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
		}

		if (playerRb.velocity.x < -0.1f)
        {
			playerSr.flipX = true;
		}
		else if (playerRb.velocity.x > 0.1f)
        {
			playerSr.flipX = false;
        }

		
	}

	void FixedUpdate()
	{
		if (Input.GetButton("Run") && Time.time - sprintTimer > sprintDelay && isGrounded || jumpedDuringSprint)
		{
			

			atMaxSpeed = true;
			playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * sprintMultiplier, playerRb.velocity.y);

			

			if (playerJumped)
			{
				jumpedDuringSprint = true; 
			}
		}

		else
		{
			atMaxSpeed = false;
			playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, playerRb.velocity.y);
		}

		//if (isGrounded && atMaxSpeed)
		//{
		//	ChangeAnimationState(RUN);
		//}

		if (playerJumped)
		{
			
			playerRb.AddForce(new Vector2(0, initialJumpForce));

			if (!isGrounded)
			{
				ChangeAnimationState(JUMP);
			}
			playerJumped = false;
		}

		if (playerJumping && Time.time - jumpTimer > delayToExtraJumpForce)
		{
			playerRb.AddForce(new Vector2(0, extraJumpForce));
			if (!isGrounded)
			{
				ChangeAnimationState(JUMP);
			}
		}

		if (isGrounded)
		{
			if (playerRb.velocity.x != 0 && !atMaxSpeed)
			{
				ChangeAnimationState(WALK);
			}
			if (playerRb.velocity.x != 0 && atMaxSpeed)
			{
				ChangeAnimationState(RUN);
			}
			else if(playerRb.velocity.x == 0)
			{
				ChangeAnimationState(IDLE);
			}
		}

		if (playerRb.velocity.y < 0 && !isGrounded)
        {
			ChangeAnimationState(FALL);
        }
	}

	void ChangeAnimationState(string newState)
    {
		if (currentState == newState) return;

		playerAnim.Play(newState);

		currentState = newState;
    }
}
