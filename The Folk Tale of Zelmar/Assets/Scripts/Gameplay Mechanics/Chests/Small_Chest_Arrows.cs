using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Chest_Arrows : MonoBehaviour
{

    bool isOpen;
    public bool atChest;

    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject arrows;

    Animator chestAnim;
    string currentState;

    const string OPEN = "Chest_Open";



    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
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
                if(!isOpen)
                {
                    Open_Chest();
                }

            }
        }
    }

    public void Open_Chest()
    {
         
                ChangeAnimationState(OPEN);
                isOpen = true;
                GameObject arrow = Instantiate(arrows, spawnPoint.position, Quaternion.identity);
                Destroy(arrow, 1f);
           
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