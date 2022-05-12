using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Chest_Bottle_Green : MonoBehaviour
{
    bool isOpen;
    public bool atChest;

    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bottle_Green_Chest_Prefab;

    Animator chestAnim;
    string currentState;

    const string OPEN = "Chest_Open";

    Inventory_Controller inventory_Controller_Script;
    [SerializeField] GameObject UI_Canvas;

    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
        inventory_Controller_Script = UI_Canvas.GetComponent<Inventory_Controller>();
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
        GameObject item = Instantiate(bottle_Green_Chest_Prefab, spawnPoint.position, Quaternion.identity);
        inventory_Controller_Script.hasBottleGreen = true;
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
