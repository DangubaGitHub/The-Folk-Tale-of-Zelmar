using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Chest_Ice : MonoBehaviour
{
    bool isOpen;
    public bool atChest;


    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject ice;

    Animator chestAnim;
    string currentState;

    const string OPEN = "Big_Chest_Open";

    Pick_Ups pick_Ups_Script;
    [SerializeField] GameObject player;

    Inventory_Controller inventory_Controller_Script;
    [SerializeField] GameObject uiCanvas;

    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
        pick_Ups_Script = player.GetComponent<Pick_Ups>();
        inventory_Controller_Script = uiCanvas.GetComponent<Inventory_Controller>();
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
                if (!isOpen && pick_Ups_Script.hasBigKey)
                {
                    Open_Chest();
                }
                else
                    Debug.Log("Locked: You need a Big Key");
            }
        }
    }

    public void Open_Chest()
    {

        ChangeAnimationState(OPEN);
        isOpen = true;
        GameObject item = Instantiate(ice, spawnPoint.position, Quaternion.identity);
        inventory_Controller_Script.hasIce = true;
        pick_Ups_Script.hasBigKey = false;
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