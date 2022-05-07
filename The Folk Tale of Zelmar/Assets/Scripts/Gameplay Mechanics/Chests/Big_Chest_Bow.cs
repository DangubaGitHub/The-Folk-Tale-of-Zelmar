using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Chest_Bow : MonoBehaviour
{
    bool atChest;
    bool isOpen;
    //bool isClosed;
    public bool hasBigKey;

    [SerializeField] GameObject bowChest;
    [SerializeField] Transform spawnPoint;

    string currentState;
    Animator chestAnim;

    const string Close = "Big_Chest_Closed";
    const string OPEN = "Big_Chest_Open";

    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
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
                if(!isOpen && hasBigKey)
                {
                    isOpen = true;
                    //isClosed = false; 
                    ChangeAnimationState(OPEN);
                    GameObject bow = Instantiate(bowChest, spawnPoint.position, Quaternion.identity);
                    Destroy(bow, 1f);
                    Debug.Log("Chest is Open");
                    hasBigKey = false;
                }

                else if(!isOpen && !hasBigKey)
                {
                    Debug.Log("Chest is Locked");
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