using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator bombAnim;
    string currentState;

    const string BOMB = "Bomb";

    private void Awake()
    {
        bombAnim = GetComponent<Animator>();
    }

    void Start()
    {
        
        ChangeAnimationState(BOMB);
        
    }


    void Update()
    {
        Destroy(transform.parent.gameObject, 2.2f);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        bombAnim.Play(newState);

        currentState = newState;
    }
}
