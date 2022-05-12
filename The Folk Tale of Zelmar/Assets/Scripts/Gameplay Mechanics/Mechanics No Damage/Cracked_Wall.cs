using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracked_Wall : MonoBehaviour
{

    Animator wallAnim;
    string currentState;

    const string WALL = "Dirt_Wall_Open";

    public bool bomb_At_Wall;

    private void Awake()
    {
        wallAnim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(bomb_At_Wall)
        {
            StartCoroutine(DelayWallCrack(1.8f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            bomb_At_Wall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            bomb_At_Wall = false;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        wallAnim.Play(newState);

        currentState = newState;
    }

    IEnumerator DelayWallCrack(float time)
    {
        yield return new WaitForSeconds(time);
        ChangeAnimationState(WALL);
    }
}
