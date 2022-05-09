using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicController : MonoBehaviour
{
    
    public int maxMagic = 20;
    public int currentMagic;

    public UI_Magic_Slider magic_Slider;

    void Start()
    {
        currentMagic = maxMagic;
        magic_Slider.SetMaxMagic(maxMagic);
    }

    void Update()
    {
        
    }

   public void Use_Magic(int magic)
    {
        currentMagic -= magic;
        magic_Slider.SetMagic(currentMagic);
    }
}
