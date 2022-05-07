using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Chest_Key : MonoBehaviour
{
    bool isOpen;
    public bool atChest;

    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject key;

    Animator chestAnim;
    string currentState;

    const string OPEN = "Chest_Open";

    Pick_Ups pick_Ups_Script;
    [SerializeField] GameObject player;

    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
        pick_Ups_Script = player.GetComponent<Pick_Ups>();
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
                if (!isOpen)
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
        GameObject item = Instantiate(key, spawnPoint.position, Quaternion.identity);
        pick_Ups_Script.smallKey++;
        Destroy(item, 1f);

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
