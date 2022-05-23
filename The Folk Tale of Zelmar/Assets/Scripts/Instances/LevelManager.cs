using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    PlayerController player_Controller_Script;
    [SerializeField] GameObject Player;

    private void Awake()
    {
        instance = this;
        player_Controller_Script = Player.GetComponent<PlayerController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {
        player_Controller_Script.stopInput = true;

        yield return new WaitForSeconds(2);

    }
}
