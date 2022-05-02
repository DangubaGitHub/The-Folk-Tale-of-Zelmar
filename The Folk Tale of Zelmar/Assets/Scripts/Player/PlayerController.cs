using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	//[SerializeField] Water waterScript;
	Rigidbody2D playerRb;
	SpriteRenderer playerSr;
	Animator playerAnim;

	public float moveSpeed;
	[SerializeField] float underwaterMoveSpeed;
	[SerializeField] float swimSpeed;

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
     
	[SerializeField] bool isGrounded;
	[SerializeField] bool inWater;
	[SerializeField] bool onLand;
	

	[SerializeField] Transform groundPoint;
	[SerializeField] Transform waterPoint;
	[SerializeField] LayerMask whatIsWater;
	[SerializeField] LayerMask whatIsGround;

	const string IDLE = "Player_Idle";
	const string WALK = "Player_Walk";
	const string RUN = "Player_Run";
	const string JUMP = "Player_Jump";
	const string FALL = "Player_Fall";
	const string HEADBUTT = "Player_Headbutt";

	string currentState;

	bool atMaxSpeed;

	[SerializeField] Transform bombPoint;
	float bombPointPosition;

	private void Awake()
    {
		playerRb = GetComponent<Rigidbody2D>();
		playerSr = GetComponent<SpriteRenderer>();
		playerAnim = GetComponent<Animator>();
		bombPointPosition = bombPoint.position.x;
	}

    private void Start()
    {
		atMaxSpeed = false;
		inWater = false;
		onLand = true;
	}

    void Update()
	{
		//inWater = Physics2D.OverlapCircle(waterPoint.position, .2f, whatIsWater);

		if(inWater)
        {
			onLand = false;
        }

		if (onLand)
		{
			isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

			

			if (playerRb.velocity.y > 0.1)
			{
				isGrounded = false;
			}

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


			Vector3 characterScale = transform.localScale;

			if (playerRb.velocity.x < -0.1f)
			{
				characterScale.x = -1;
				//playerSr.flipX = true;
				
			}
			else if (playerRb.velocity.x > 0.1f)
			{

				characterScale.x = 1;
				//playerSr.flipX = false;
				
			}
			transform.localScale = characterScale;
		}
		
	}

	void FixedUpdate()
	{
		if (onLand)
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
				else if (playerRb.velocity.x == 0)
				{
					ChangeAnimationState(IDLE);
				}
			}

			if (playerRb.velocity.y < 0 && !isGrounded)
			{
				ChangeAnimationState(FALL);
			}
		}

		if(inWater)
        {
			playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * underwaterMoveSpeed * Time.deltaTime, playerRb.velocity.y);

			if (Input.GetButtonDown("Jump"))
            {
				playerRb.AddForce(new Vector2(0, swimSpeed));
			}


		}
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == whatIsWater)
        {
			inWater = true;
        }
    }

    

    void ChangeAnimationState(string newState)
    {
		if (currentState == newState) return;

		playerAnim.Play(newState);

		currentState = newState;
    }
}
