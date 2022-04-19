using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    [SerializeField] PlayerController playerControllerScript;

   

    private void Awake()
    {
       
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Water"))
        {
            playerControllerScript.inWater = true;
            playerControllerScript.onLand = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Water"))
        {
            playerControllerScript.inWater = false;
            playerControllerScript.onLand = true;
        }
    }



}
