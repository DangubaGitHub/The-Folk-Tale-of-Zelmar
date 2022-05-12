using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier_Torch : MonoBehaviour
{

    Animator torchAnim;
    string currentState;
    const string TORCH_FIRE = "Torch_Burning";

    public bool torchLitt;

    private void Awake()
    {
        torchAnim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire Ball"))
        {
            ChangeAnimationState(TORCH_FIRE);
            torchLitt = true;
            Destroy(other.gameObject);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        torchAnim.Play(newState);

        currentState = newState;
    }
}
