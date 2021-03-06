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

	public bool stopInput;

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
	public bool useBottleRed;
	public bool useBottleGreen;

	////////////////////////////// PREFABS //////////

	[Header("Prefabs")]
	[SerializeField] GameObject bombPrefab;
	[SerializeField] GameObject arrowPrefab;
	[SerializeField] GameObject firePrefab;
	[SerializeField] GameObject icePrefab;

	////////////////////////////// OTHER SCRIPTS //////////

	[Header("Scripts")]
	[SerializeField] GameObject Player;
	[SerializeField] GameObject UI_Canvas;
	[SerializeField] GameObject UI_Magic_Bar;

	Inventory_Controller inventory_Controller_Script;
	UI_Controller uI_Controller_Script;
	Pick_Ups pick_Ups_Script;
	PlayerMagicController magic_Controller_Script;
	PlayerHealthController player_Health_Controller_Script;
	UI_Magic_Slider uI_Magic_Slider_Script;
	

	private void Awake()
    {
		playerRb = GetComponent<Rigidbody2D>();
		playerSr = GetComponent<SpriteRenderer>();
		playerAnim = GetComponent<Animator>();
		inventory_Controller_Script = UI_Canvas.GetComponent<Inventory_Controller>();
		uI_Controller_Script = UI_Canvas.GetComponent<UI_Controller>();
		pick_Ups_Script = Player.GetComponent<Pick_Ups>();
		magic_Controller_Script = Player.GetComponent<PlayerMagicController>();
		player_Health_Controller_Script = Player.GetComponent<PlayerHealthController>();
		uI_Magic_Slider_Script = UI_Magic_Bar.GetComponent<UI_Magic_Slider>();
	}

    private void Start()
    {
		atMaxSpeed = false;
		inWater = false;
		onLand = true;
	}

    void Update() ////////////////////////////////////////////////////////////////////////////////////////// UPDATE //////////////////////////////
	{
		if (inventory_Controller_Script.inventoryOn == false && !stopInput)
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

					if (useBomb && pick_Ups_Script.bombs > 0)
					{
						Instantiate(bombPrefab, bombPoint.position, bombPoint.rotation);
						pick_Ups_Script.bombs--;
					}
					else if (useBomb && pick_Ups_Script.bombs <= 0)
					{
						Debug.Log("No Bombs");
						//Play Sound
                    }

					if (useFire && magic_Controller_Script.currentMagic > 0)
					{
						ChangeAnimationState(MAGIC);
						Instantiate(firePrefab, magicPoint.position, magicPoint.rotation);

						magic_Controller_Script.Use_Magic(5);
					}
					else if(useFire && magic_Controller_Script.currentMagic <= 0)
                    {
						ChangeAnimationState(MAGIC);
						Debug.Log("No Magic");
						//Play Sound
					}

					if (useIce && magic_Controller_Script.currentMagic > 0)
					{
						ChangeAnimationState(MAGIC);
						Instantiate(icePrefab, magicPoint.position, magicPoint.rotation);

						magic_Controller_Script.Use_Magic(10);
					}
					else if(useIce && magic_Controller_Script.currentMagic <= 0)
                    {
						ChangeAnimationState(MAGIC);
						Debug.Log("No Magic");
						//Play Sound
					}

					if (useBow && pick_Ups_Script.arrows > 0)
					{
						ChangeAnimationState(BOW);
						Instantiate(arrowPrefab, arrowPoint.position, arrowPoint.rotation);
						pick_Ups_Script.arrows--;
					}
					else if(useBow && pick_Ups_Script.arrows <= 0)
                    {
						ChangeAnimationState(BOW);
						Debug.Log("No Arrows");
						//Play Sound
                    }

					if (useBottleRed && player_Health_Controller_Script.currentHealth < player_Health_Controller_Script.maxHealth)
					{
						player_Health_Controller_Script.currentHealth = player_Health_Controller_Script.maxHealth;
						uI_Controller_Script.UpdateHealthDisplay();
						inventory_Controller_Script.hasBottleRed = false;
						inventory_Controller_Script.bottle_Red_Icon.SetActive(false);
					}

					if (useBottleGreen && magic_Controller_Script.currentMagic < magic_Controller_Script.maxMagic)
					{
						magic_Controller_Script.currentMagic = magic_Controller_Script.maxMagic;
						uI_Magic_Slider_Script.SetMaxMagic(magic_Controller_Script.maxMagic);
						inventory_Controller_Script.hasBottleGreen = false;
						inventory_Controller_Script.bottle_Green_Icon.SetActive(false);
					}

					Invoke("AttackComplete", 0.4f);
				}

				else if(!isAirAttacking && !isGrounded) //////////////////////////////////////////////////////////// AIR ATTACK //////////////////////////////
				{
					isAirAttacking = true;

					if (useBomb && pick_Ups_Script.bombs > 0)
					{
						Instantiate(bombPrefab, bombPoint.position, bombPoint.rotation);
						pick_Ups_Script.bombs--;
					}
					else if (useBomb && pick_Ups_Script.bombs <= 0)
					{
						Debug.Log("No Bombs");
						//Play Sound
					}

					if (useFire)
					{
						if (playerRb.velocity.y < 0)
						{
							if (magic_Controller_Script.currentMagic > 0)
							{
								ChangeAnimationState(MAGIC_FALL);
								Instantiate(firePrefab, magicPoint.position, magicPoint.rotation);

								magic_Controller_Script.Use_Magic(5);
							}
							else if(magic_Controller_Script.currentMagic <= 0)
                            {
								ChangeAnimationState(MAGIC_FALL);
								Debug.Log("No Magic");
								//Play Sound
							}
						}
						else if(playerRb.velocity.y > 0)
                        {
							if (magic_Controller_Script.currentMagic > 0)
							{
								ChangeAnimationState(MAGIC_JUMP);
								Instantiate(firePrefab, magicPoint.position, magicPoint.rotation);

								magic_Controller_Script.Use_Magic(5);
							}
							else if(magic_Controller_Script.currentMagic <= 0)
                            {
								ChangeAnimationState(MAGIC_JUMP);
								Debug.Log("No Magic");
								//Play Sound
							}
						}
					}

					if (useIce)
					{
						if (playerRb.velocity.y < 0)
						{
							if (magic_Controller_Script.currentMagic > 0)
							{
								ChangeAnimationState(MAGIC_FALL);
								Instantiate(icePrefab, magicPoint.position, magicPoint.rotation);

								magic_Controller_Script.Use_Magic(10);
							}
							else  if(magic_Controller_Script.currentMagic <= 0)
                            {
								ChangeAnimationState(MAGIC_FALL);
								Debug.Log("No Magic");
								//Play Sound
							}
						}
						else if (playerRb.velocity.y > 0)
						{
							if (magic_Controller_Script.currentMagic > 0)
							{
								ChangeAnimationState(MAGIC_JUMP);
								Instantiate(icePrefab, magicPoint.position, magicPoint.rotation);

								magic_Controller_Script.Use_Magic(10);
							}
							else if(magic_Controller_Script.currentMagic <= 0)
                            {
								ChangeAnimationState(MAGIC_JUMP);
								Debug.Log("No Magic");
								//Play Sound
							}
						}
				    }

					if (useBow && pick_Ups_Script.arrows > 0)
					{
						if (playerRb.velocity.y < 0)
						{
							ChangeAnimationState(BOW_FALL);
							Instantiate(arrowPrefab, arrowPoint.position, arrowPoint.rotation);
							pick_Ups_Script.arrows--;
						}
						else if (playerRb.velocity.y > 0)
						{
							ChangeAnimationState(BOW_JUMP);
							Instantiate(arrowPrefab, arrowPoint.position, arrowPoint.rotation);
							pick_Ups_Script.arrows--;
						}
					}
					else if (useBow && pick_Ups_Script.arrows <= 0)
					{
						if (playerRb.velocity.y < 0)
						{
							ChangeAnimationState(BOW_FALL);
						}
						else if (playerRb.velocity.y > 0)
						{
							ChangeAnimationState(BOW_JUMP);
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
		if (onLand && !stopInput)
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
