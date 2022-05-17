using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back_Door : MonoBehaviour
{
    bool atDoor;

    void Start()
    {
        
    }

    void Update()
    {
        if(atDoor)
        {
            if(Input.GetButtonDown("Enter"))
            {
                SceneManager.LoadScene("");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            atDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            atDoor = false;
        }
    }
}
