using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    UI_Controller ui_Controller_Script;
    [SerializeField] GameObject ui_Canvas;

    public int currentHealth;
    public int maxHealth;

    private void Awake()
    {
        ui_Controller_Script = ui_Canvas.GetComponent<UI_Controller>();
    }

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
            currentHealth = 0;

            gameObject.SetActive(false);
        }

        ui_Controller_Script.UpdateHealthDisplay();
    }
}
