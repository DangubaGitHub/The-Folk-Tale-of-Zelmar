using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Gold_Pickup : MonoBehaviour
{
    bool isCollected;

    Pick_Ups pick_Ups_Script;
    [SerializeField] GameObject player;

    private void Awake()
    {
        pick_Ups_Script = player.GetComponent<Pick_Ups>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            pick_Ups_Script.coinCount += 10;
            isCollected = true;
            Destroy(gameObject);
        }
    }
}
