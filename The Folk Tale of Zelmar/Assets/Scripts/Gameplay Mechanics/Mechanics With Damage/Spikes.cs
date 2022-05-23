using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    PlayerHealthController player_Health_Controller_Script;
    [SerializeField] GameObject player;

    private void Awake()
    {
        player_Health_Controller_Script = player.GetComponent<PlayerHealthController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player_Health_Controller_Script.DealDamage();
        }
    }
}
