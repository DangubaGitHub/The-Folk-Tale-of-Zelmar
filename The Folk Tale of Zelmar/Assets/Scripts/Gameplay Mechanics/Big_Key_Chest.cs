using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Key_Chest : MonoBehaviour
{
    Big_Chest big_Chest_Script;
    [SerializeField] GameObject big_Chest;

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
        big_Chest_Script = big_Chest.GetComponent<Big_Chest>();
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
                    big_Chest_Script.hasBigKey = true;
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
