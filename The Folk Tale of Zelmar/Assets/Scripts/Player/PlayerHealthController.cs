using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void DealDamage()
    {
        currentHealth--;

        if(currentHealth <=0)
        {
            gameObject.SetActive(false);
        }
    }
}
