using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Magic_Slider : MonoBehaviour
{
    [SerializeField] Slider magicSlider;

    public void SetMaxMagic(int magic)
    {
        magicSlider.maxValue = magic;
        magicSlider.value = magic;
    }

    public void SetMagic(int magic)
    {
        magicSlider.value = magic;
    }
}
