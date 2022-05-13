using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier_Torch : MonoBehaviour
{

    Animator torchAnim;
    string currentState;
    const string TORCH_FIRE = "Torch_Burning";

    public bool torchLitt;

    Wood_Blockade wood_Blockade_Script;
    [SerializeField] GameObject wood_Blockade;

    private void Awake()
    {
        torchAnim = GetComponent<Animator>();
        wood_Blockade_Script = wood_Blockade.GetComponent<Wood_Blockade>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire Ball") && !torchLitt)
        {
            ChangeAnimationState(TORCH_FIRE);
            torchLitt = true;
            Destroy(other.gameObject);

            if (wood_Blockade_Script.torch_1_Litt == false)
            {
                wood_Blockade_Script.torch_1_Litt = true;
            }
            else if(wood_Blockade_Script.torch_2_Litt == false)
            {
                wood_Blockade_Script.torch_2_Litt = true;
            }
            else if(wood_Blockade_Script.torch_3_Litt == false)
            {
                wood_Blockade_Script.torch_3_Litt = true;
            }
            else if(wood_Blockade_Script.torch_4_Litt == false)
            {
                wood_Blockade_Script.torch_4_Litt = true;
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        torchAnim.Play(newState);

        currentState = newState;
    }
}
