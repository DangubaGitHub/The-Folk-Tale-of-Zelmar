using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Button : MonoBehaviour
{
    public bool isPushed;

    BoxCollider2D boxCollider2D;
    CapsuleCollider2D capsuleCollider2D;

    Animator buttonAnim;
    string currentState;

    const string PUSHED = "Red_Buton_Pushed";

    private void Awake()
    {
        buttonAnim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("DelayColliderEnabling", 0.3f);
            
            ChangeAnimationState(PUSHED);

            isPushed = true;
        }
    }

    void DelayColliderEnabling()
    {
        boxCollider2D.enabled = false;
        capsuleCollider2D.enabled = false;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        buttonAnim.Play(newState);

        currentState = newState;
    }
}
