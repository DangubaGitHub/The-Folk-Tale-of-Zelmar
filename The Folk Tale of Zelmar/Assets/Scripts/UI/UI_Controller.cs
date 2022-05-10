using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    PlayerHealthController player_Health_Controller_Script;
    Key_Manager key_Manager_Script;
    Pick_Ups pick_Ups_Script;
    [SerializeField] GameObject player;

    [SerializeField] Image heart1;
    [SerializeField] Image heart2;
    [SerializeField] Image heart3;

    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartEmpty;

    [SerializeField] Text arrowsCount;
    [SerializeField] Text bombsCount;
    [SerializeField] Text cointCount;
    [SerializeField] Text smallKey;

    public GameObject bigKey;


    private void Awake()
    {
        player_Health_Controller_Script = player.GetComponent<PlayerHealthController>();
        pick_Ups_Script = player.GetComponent<Pick_Ups>();
        key_Manager_Script = player.GetComponent<Key_Manager>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void UpdateHealthDisplay()
    {
        switch (player_Health_Controller_Script.currentHealth)
        {
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;

            case 1:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }

    public void UpdateCoinCount()
    {
        cointCount.text = pick_Ups_Script.coinCount.ToString();
    }

    public void UpdateArrowCount()
    {
        arrowsCount.text = pick_Ups_Script.arrows.ToString();
    }

    public void UpdateBombsCount()
    {
        bombsCount.text = pick_Ups_Script.bombs.ToString();
    }

    public void UpdateSmallKeyCount()
    {
        smallKey.text = key_Manager_Script.smallKey.ToString();
    }

}
