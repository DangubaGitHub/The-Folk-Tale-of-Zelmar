using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] GameObject bottleRed;
    public bool hasBottleRed;

    [SerializeField] GameObject bottleGreen;
    public bool hasBottleGreen;

    //[SerializeField] GameObject bottleEmpty;
    //public bool hasBottleEmpty;

    [SerializeField] GameObject inventory;
    public bool inventoryOn;

    [Header("UI Item Display")]
    [SerializeField] GameObject bomb_Icon;
    [SerializeField] GameObject fire_Icon;
    [SerializeField] GameObject ice_Icon;
    [SerializeField] GameObject bottle_Red_Icon;
    [SerializeField] GameObject bottle_Green_Icon;
    //[SerializeField] GameObject bottle_Empty_Icon;
    [SerializeField] GameObject bow_Icon;

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

        if(!hasBottleRed)
        {
            bottleRed.SetActive(false);
        }
        else
        {
            bottleRed.SetActive(true);
        }

        if (!hasBottleGreen)
        {
            bottleGreen.SetActive(false);
        }
        else
        {
            bottleGreen.SetActive(true);
        }

      /*  if (!hasBottleEmpty)
        {
            bottleEmpty.SetActive(false);
            bottle_Empty_Icon.SetActive(false);
        }
        else
        {
            bottleEmpty.SetActive(true);
            bottle_Empty_Icon.SetActive(true);
        }*/
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
        player_Controller_Script.useBottleRed = false;
        player_Controller_Script.useBottleGreen = false;

        bomb_Icon.SetActive(true);
        bow_Icon.SetActive(false);
        fire_Icon.SetActive(false);
        ice_Icon.SetActive(false);
        bottle_Red_Icon.SetActive(false);
        bottle_Green_Icon.SetActive(false);
    }

    public void Bow()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = true;
        player_Controller_Script.useFire = false;
        player_Controller_Script.useIce = false;
        player_Controller_Script.useBottleRed = false;
        player_Controller_Script.useBottleGreen = false;

        //bomb_Icon.SetActive(false);
        bow_Icon.SetActive(true);
        fire_Icon.SetActive(false);
        ice_Icon.SetActive(false);
        bottle_Red_Icon.SetActive(false);
        bottle_Green_Icon.SetActive(false);
    }

    public void Fire()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = false;
        player_Controller_Script.useFire = true;
        player_Controller_Script.useIce = false;
        player_Controller_Script.useBottleRed = false;
        player_Controller_Script.useBottleGreen = false;

        //bomb_Icon.SetActive(false);
        bow_Icon.SetActive(false);
        fire_Icon.SetActive(true);
        ice_Icon.SetActive(false);
        bottle_Red_Icon.SetActive(false);
        bottle_Green_Icon.SetActive(false);
    }

    public void Ice()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = false;
        player_Controller_Script.useFire = false;
        player_Controller_Script.useIce = true;
        player_Controller_Script.useBottleRed = false;
        player_Controller_Script.useBottleGreen = false;

        //bomb_Icon.SetActive(false);
        bow_Icon.SetActive(false);
        fire_Icon.SetActive(false);
        ice_Icon.SetActive(true);
        bottle_Red_Icon.SetActive(false);
        bottle_Green_Icon.SetActive(false);
    }

    public void BottleRed()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = false;
        player_Controller_Script.useFire = false;
        player_Controller_Script.useIce = false;
        player_Controller_Script.useBottleRed = true;
        player_Controller_Script.useBottleGreen = false;

        //bomb_Icon.SetActive(false);
        bow_Icon.SetActive(false);
        fire_Icon.SetActive(false);
        ice_Icon.SetActive(false);
        bottle_Red_Icon.SetActive(true);
        bottle_Green_Icon.SetActive(false);
    }

    public void BottleGreen()
    {
        player_Controller_Script.useBomb = false;
        player_Controller_Script.useBow = false;
        player_Controller_Script.useFire = false;
        player_Controller_Script.useIce = false;
        player_Controller_Script.useBottleRed = false;
        player_Controller_Script.useBottleGreen = true;

        //bomb_Icon.SetActive(false);
        bow_Icon.SetActive(false);
        fire_Icon.SetActive(false);
        ice_Icon.SetActive(false);
        bottle_Red_Icon.SetActive(false);
        bottle_Green_Icon.SetActive(true);
    }
}
