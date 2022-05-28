using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Area_Exit : MonoBehaviour
{
   
    public string areaToLoad;

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
            SceneManager.LoadScene(areaToLoad);
        }
    }
}
