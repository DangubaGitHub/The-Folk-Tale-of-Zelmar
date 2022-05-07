using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions : MonoBehaviour
{

    public bool haskey;
    public bool hasBigKey;
    public bool atSmallChest;
    public bool atBigChest;
    public bool atDoor;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Small Chest"))
        {
            atSmallChest = true;
        }

        if(other.CompareTag("Big Chest"))
        {
            atBigChest = true;
        }

        if(other.CompareTag("Door"))
        {
            atDoor = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Small Chest"))
        {
            atSmallChest = false;
        }

        if(other.CompareTag("Big Chest"))
        {
            atBigChest = false;
        }

        if(other.CompareTag("Door"))
        {
            atDoor = false;
        }
    }
}
