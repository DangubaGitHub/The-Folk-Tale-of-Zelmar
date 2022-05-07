using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Ups : MonoBehaviour
{

    public int smallKey;
    public int bombs;
    public int arrows;
    public int coinCount;

    public bool bigKey;

    void Start()
    {
        
    }

    void Update()
    {
        
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
