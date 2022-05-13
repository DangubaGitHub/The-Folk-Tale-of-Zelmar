using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Blockade : MonoBehaviour
{
    //[SerializeField] GameObject torch_1;
    //[SerializeField] GameObject torch_2;
    //[SerializeField] GameObject torch_3;
    //[SerializeField] GameObject torch_4;

    Rigidbody2D rb2d;

    Brazier_Torch brazier_Torch_Script;

    public bool torch_1_Litt;
    public bool torch_2_Litt;
    public bool torch_3_Litt;
    public bool torch_4_Litt;

    private void Awake()
    {
        //brazier_Torch_Script = torch_1.GetComponent<Brazier_Torch>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        if(torch_1_Litt == true && torch_2_Litt == true && torch_3_Litt == true && torch_4_Litt == true)
        {
            rb2d.gravityScale = 0.5f;
            
            Destroy(gameObject, 1.5f);
        }
    }
}
