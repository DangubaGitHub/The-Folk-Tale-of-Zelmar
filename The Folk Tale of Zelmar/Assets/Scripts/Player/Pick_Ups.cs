using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour
{

    public int smallKey;
    public int bombs;
    public int arrows;
    public int coinCount;

    public bool hasBigKey;

    Inventory_Controller inventory_Controller_Script;
    [SerializeField] GameObject uiCanvas;

    private void Awake()
    {
        inventory_Controller_Script = uiCanvas.GetComponent<Inventory_Controller>();
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
        if(other.CompareTag("Bronze Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Silver Coin"))
        {
            coinCount = coinCount + 5;
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Gold Coin"))
        {
            coinCount = coinCount + 10;
            Destroy(other.gameObject);
        }
    }
}
