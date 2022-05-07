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

	bool atMaxSpeed;
	bool isAttacking;
	bool isAirAttacking;

	[SerializeField] Transform groundPoint;
	[SerializeField] Transform waterPoint;
	[SerializeField] LayerMask whatIsWater;
	[SerializeField] LayerMask whatIsGround;

	////////////////////////////// ANIMATION //////////

	const string IDLE = "Player_Idle";
	const string WALK = "Player_Walk";
	const string RUN = "Player_Run";
	const string JUMP = "Player_Jump";
	const string FALL = "Player_Fall";
	const string HEADBUTT = "Player_Headbutt";
	const string MAGIC = "Player_Magic";
	const string MAGIC_JUMP = "Player_Magic_Jump";
	const string MAGIC_FALL = "Player_Magic_Fall";
	const string BOW = "Player_Bow";
	const string BOW_JUMP = "Player_Bow_Jump";
	const string BOW_FALL = "Player_Bow_Fall";

	string currentState;

	////////////////////////////// FIRE POINTS //////////

	[Header("Fire Points")]
	[SerializeField] Transform bombPoint;
	[SerializeField] Transform magicPoint;
	[SerializeField] Transform arrowPoint;
	//float bombPointPosition;

	////////////////////////////// INVENTORY //////////    

	[Header("Inventory Items")]
	public bool useBomb;
	public bool useBow;
	public bool useFire;
	public bool useIce;
	public bool useBottle;

	////////////////////////////// PREFABS //////////

	[Header("Prefabs")]
	[SerializeField] GameObject bombPrefab;
	[SerializeField] GameObject arrowPrefab;
	[SerializeField] GameObject firePrefab;
	[SerializeField] GameObject icePrefab;

	////////////////////////////// OTHER SCRIPTS //////////

	[Header("Scripts")]
	[SerializeField] GameObject inventory;
	[SerializeField] GameObject smallChestBombs;
	[SerializeField] GameObject smallChestArrows;

	
	Inventory_Controller inventory_Controller_Script;
    Small_Chest_Bombs small_Chest_Bombs;
	Small_Chest_Arrows small_Chest_Arrows;
    Player_Collisions player_Collisions;

	private void Awake()
    {
		player_Collisions = gameObject.GetComponent<Player_Collisions>();

		playerRb = GetComponent<Rigidbody2D>();
		playerSr = GetComponent<SpriteRenderer>();
		playerAnim = GetComponent<Animator>();
		inventory_Controller_Script = inventory.GetComponent<Inventory_Controller>();
		small_Chest_Bombs = smallChestBombs.GetComponent<Small_Chest_Bombs>();
		small_Chest_Arrows = smallChestBombs.GetComponent<Small_Chest_Arrows>();
		//bombPointPosition = bombPoint.position.x;
	}

    private void Start()
    {
		atMaxSpeed = false;
		inWater = false;
		onLand = true;
	}

    void Update() ////////////////////////////////////////////////////////////////////////////////////////// UPDATE //////////////////////////////
	{
		if (inventory_Controller_Script.inventoryOn == false)
		{
			//inWater = Physics2D.OverlapCircle(waterPoint.position, .2f, whatIsWater);

			if (inWater)
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

				if (Input.GetButtonDown("Jump") && isGrounded && !isAirAttacking)
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
					//playerSr.transform.eulerAngles = new Vector3(0, 180, 0);
					//playerSr.flipX = true;
					bombPoint.transform.eulerAngles = new Vector3(0, 180, 0);
					arrowPoint.transform.eulerAngles = new Vector3(0, 180, 0);
					magicPoint.transform.eulerAngles = new Vector3(0, 180, 0);
				}
				else if (playerRb.velocity.x > 0.1f)
				{
					

					characterScale.x = 1;
					//playerSr.transform.eulerAngles = new Vector3(0, 0, 0);
					//playerSr.flipX = false;
					bombPoint.transform.eulerAngles = new Vector3(0, 0, 0);
					arrowPoint.transform.eulerAngles = new Vector3(0, 0, 0);
					magicPoint.transform.eulerAngles = new Vector3(0, 0, 0);
				}
				transform.localScale = characterScale;
			}

			if (Input.GetButtonDown("Use Item")) //////////////////////////////////////////////////////////// GROUND ATTACK //////////////////////////////
			{
				if (!isAttacking && isGrounded)
				{
					isAttacking = true;

					if (useBomb)
					{
						Instantiate(bombPrefab, bombPoint.position, bombPoint.rotation);
					}

					if (useFire)
					{
						ChangeAnimationState(MAGIC);
						Instantiate(firePrefab, magicPoint.position, magicPoint.rotation);
					}

					if (useIce)
					{
						ChangeAnimationState(MAGIC);
						Instantiate(icePrefab, magicPoint.position, magicPoint.rotation);
					}

					if (useBow)
					{
						ChangeAnimationState(BOW);
						Instantiate(arrowPrefab, arrowPoint.position, arrowPoint.rotation);
					}

					Invoke("AttackComplete", 0.4f);
				}

				else if(!isAirAttacking && !isGrounded) //////////////////////////////////////////////////////////// AIR ATTACK //////////////////////////////
				{
					isAirAttacking = true;

					if (useBomb)
					{
						Instantiate(bombPrefab, bombPoint.position, bombPoint.rotation);
					}

					if (useFire)
					{
						if (playerRb.velocity.y < 0)
						{
							ChangeAnimationState(MAGIC_FALL);
							Instantiate(firePrefab, magicPoint.position, magicPoint.rotation);
						}
						else if(playerRb.velocity.y > 0)
                        {
							ChangeAnimationState(MAGIC_JUMP);
							Instantiate(firePrefab, magicPoint.position, magicPoint.rotation);
						}
					}

					if (useIce)
					{
						if (playerRb.velocity.y < 0)
						{
							ChangeAnimationState(MAGIC_FALL);
							Instantiate(icePrefab, magicPoint.position, magicPoint.rotation);
						}
						else if (playerRb.velocity.y > 0)
						{
							ChangeAnimationState(MAGIC_JUMP);
							Instantiate(icePrefab, magicPoint.position, magicPoint.rotation);
						}
				    }

					if (useBow)
					{
						if (playerRb.velocity.y < 0)
						{
							ChangeAnimationState(BOW_FALL);
							Instantiate(arrowPrefab, arrowPoint.position, arrowPoint.rotation);
						}
						else if (playerRb.velocity.y > 0)
						{
							ChangeAnimationState(BOW_JUMP);
							Instantiate(arrowPrefab, arrowPoint.position, arrowPoint.rotation);
						}
					}

					Invoke("AttackComplete", 0.4f);
				}
			}
			//isAttacking = false;
		}

		if (isGrounded)
		{
			if (Input.GetButtonDown("Inventory"))
			{
				inventory_Controller_Script.Inventory_On_Off();
			}
		}
	}

	void FixedUpdate() ////////////////////////////////////////////////////////////////////////////////////////// FIXED UPDATE //////////////////////////////
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

				if (!isGrounded && !isAirAttacking)
				{
					ChangeAnimationState(JUMP);
				}
				playerJumped = false;
			}

			if (playerJumping && Time.time - jumpTimer > delayToExtraJumpForce)
			{
				playerRb.AddForce(new Vector2(0, extraJumpForce));

				if (!isGrounded && !isAirAttacking)
				{
					ChangeAnimationState(JUMP);
				}
			}

			if (isGrounded && !isAttacking)
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

			if (playerRb.velocity.y < 0 && !isGrounded && !isAirAttacking)
			{
				ChangeAnimationState(FALL);
			}

			////////////////////////////////////////////////////////////////////////////////////////// USE BUTTON //////////////////////////////

			
		}
	}

    void ChangeAnimationState(string newState)
    {
		if (currentState == newState) return;

		playerAnim.Play(newState);

		currentState = newState;
    }

	void AttackComplete()
    {
		isAttacking = false;
		isAirAttacking = false;
    }
}
