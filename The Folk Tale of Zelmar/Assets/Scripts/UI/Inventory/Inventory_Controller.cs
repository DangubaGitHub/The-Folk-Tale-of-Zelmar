using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Controller : MonoBehaviour
{
    PlayerController player_Controller_Script;
    [SerializeField] GameObject player;

    [SerializeField] GameObject bomb;
    public bool hasBomb;

    [SerializeField] GameObject bow;
    public bool hasBow;

    [SerializeField] GameObject fire;
    public bool hasFire;

    [SerializeField] GameObject ice;
    public bool hasIce;

    [SerializeField] GameObject bottle;
    public bool hasBottle;

    [SerializeField] GameObject inventory;
    public bool inventoryOn;

    private void Awake()
    {
        player_Controller_Script = player.GetComponent<PlayerController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(!hasBomb)
        {
            bomb.SetActive(false);
        }
        else
        {
            bomb.SetActive(true);
        }

        if(!hasBow)
        {
            bow.SetActive(false);
        }
        else
        {
            bow.SetActive(true);
        }

        if(!hasFire)
        {
            fire.SetActive(false);
        }
        else
        {
            fire.SetActive(true);
        }

        if(!hasIce)
        {
            ice.SetActive(false);
        }
        else
        {
            ice.SetActive(true);
        }

        if(!hasBottle)
        {
            bottle.SetActive(false);
        }
        else
        {
            bottle.SetActive(true);
        }
    }

    public void Inventory_On_Off()
    {
        if(inventoryOn)
        {
            inventoryOn = false;
            inventory.SetActive(false);
            Time.timeScale = 1;
        }

        else
        {
            inventoryOn = true;
            inventory.SetActive(true);
            Time.timeScale = 0;
        }
    }
       
       

    public void Bomb()
    {
        player_Controller_Script.useBomb = true;
        player_Controller_Script.useBow = false;
        player_Controller_Script.useFire = false;
        player_Controller_Script.useIce = false;
        player_Controller_Script.useBottle = false;
    }

    public void Bow()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = true;
        player_Controller_Script.useFire = false;
        player_Controller_Script.useIce = false;
        player_Controller_Script.useBottle = false;
    }

    public void Fire()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = false;
        player_Controller_Script.useFire = true;
        player_Controller_Script.useIce = false;
        player_Controller_Script.useBottle = false;
    }

    public void Ice()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = false;
        player_Controller_Script.useFire = false;
        player_Controller_Script.useIce = true;
        player_Controller_Script.useBottle = false;
    }

    public void Bottle()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = false;
        player_Controller_Script.useFire = false;
        player_Controller_Script.useIce = false;
        player_Controller_Script.useBottle = true;
    }
}
