using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Chest : MonoBehaviour
{

    Level_Door level_Door_Script;
    [SerializeField] GameObject level_Door;

    Animator chestAnim;
    string currentState;

    const string OPEN = "Chest_Open";
    const string CLOSED = "Chest_Closed";

    bool atChest;
    bool chestOpen;

    [SerializeField] GameObject keyPrefab;
    [SerializeField] Transform keySpawnPoint;

    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
        level_Door_Script = level_Door.GetComponent<Level_Door>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(atChest)
        {
            if(Input.GetButtonDown("Use"))
            {
                if(!chestOpen)
                {
                    ChangeAnimationState(OPEN);
                    chestOpen = true;
                    GameObject key = Instantiate(keyPrefab, keySpawnPoint.position, Quaternion.identity);
                    Destroy(key, 1f);
                    level_Door_Script.hasKey = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            atChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
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
