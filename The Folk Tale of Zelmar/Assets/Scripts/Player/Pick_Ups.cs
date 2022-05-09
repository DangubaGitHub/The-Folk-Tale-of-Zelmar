using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour
{

    public int smallKey;
    public int bombs;
    bool bombsCollected;
    public int arrows;
    bool arrowsCollected;
    public int coinCount;
    bool coinCollected;

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
        if(bombs > 0)
        {
            inventory_Controller_Script.hasBomb = true;
        }
        else if(bombs <= 0)
        {
            inventory_Controller_Script.hasBomb = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bronze Coin") && !coinCollected)
        {
            coinCount++;
            
            uI_Controller_Script.UpdateCoinCount();
            Destroy(other.gameObject);
            coinCollected = true;
        }

        if(other.CompareTag("Silver Coin") && !coinCollected)
        {
            coinCount = coinCount + 5;
           
            uI_Controller_Script.UpdateCoinCount();
            Destroy(other.gameObject);
            coinCollected = true;
        }

        if(other.CompareTag("Gold Coin") && !coinCollected)
        {
            coinCount = coinCount + 10;
            
            uI_Controller_Script.UpdateCoinCount();
            Destroy(other.gameObject);
            coinCollected = true;
        }
    }
}
