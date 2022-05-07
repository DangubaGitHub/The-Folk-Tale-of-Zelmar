using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Key_Chest : MonoBehaviour
{
    Big_Chest_Bow big_Chest_Bow_Script;
    [SerializeField] GameObject big_Chest_Bow;
    Big_Chest_Fire big_Chest_Fire_Script;
    [SerializeField] GameObject big_Chest_Fire;

    Animator chestAnim;
    string currentState;

    const string OPEN = "Chest_Open";
    const string CLOSED = "Chest_Closed";

    bool atChest;
    bool chestOpen;

    [SerializeField] GameObject bigKeyPrefab;
    [SerializeField] Transform keySpawnPoint;

    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
        big_Chest_Bow_Script = big_Chest_Bow.GetComponent<Big_Chest_Bow>();
        big_Chest_Fire_Script = big_Chest_Fire.GetComponent<Big_Chest_Fire>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (atChest)
        {
            if (Input.GetButtonDown("Use"))
            {
                if (!chestOpen)
                {
                    ChangeAnimationState(OPEN);
                    chestOpen = true;
                    GameObject key = Instantiate(bigKeyPrefab, keySpawnPoint.position, Quaternion.identity);
                    Destroy(key, 1f);
                   // big_Chest_Bow_Script.hasBigKey = true;
                   // big_Chest_Fire_Script.hasBigKey = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            atChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            atChest = false;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        chestAnim.Play(newState);

        currentState = newState;
    }
}
