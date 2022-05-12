using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Blockade : MonoBehaviour
{
    [SerializeField] GameObject torch_1;
    [SerializeField] GameObject torch_2;
    [SerializeField] GameObject torch_3;
    [SerializeField] GameObject torch_4;

    Brazier_Torch brazier_Torch_Script;

    private void Awake()
    {
        brazier_Torch_Script = torch_1.GetComponent<Brazier_Torch>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
