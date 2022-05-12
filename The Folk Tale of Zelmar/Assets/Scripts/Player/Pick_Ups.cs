using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour
{

    public int smallKey;
    public int bombs;
    int maxBombs = 10;
    public int arrows;
    int maxArrows = 30;
    public int coinCount;
    int maxCoins = 999;

    public bool hasBigKey;

    Inventory_Controller inventory_Controller_Script;
    [SerializeField] GameObject uiCanvas;

    UI_Controller uI_Controller_Script;
    

    private void Awake()
    {
        inventory_Controller_Script = uiCanvas.GetComponent<Inventory_Controller>();
        uI_Controller_Script = uiCanvas.GetComponent<UI_Controller>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(arrows > maxArrows)
        {
            arrows = maxArrows;
        }

        if(bombs > maxBombs)
        {
            bombs = maxBombs;
        }

        if(coinCount > maxCoins)
        {
            coinCount = maxCoins;
        }

        uI_Controller_Script.UpdateCoinCount();
        uI_Controller_Script.UpdateArrowCount();
        uI_Controller_Script.UpdateBombsCount();

        if(bombs > 0)
        {
            inventory_Controller_Script.hasBomb = true;
        }
        else if(bombs <= 0)
        {
            inventory_Controller_Script.hasBomb = false;
            inventory_Controller_Script.bomb_Icon.SetActive(false);
        }
    }
}
