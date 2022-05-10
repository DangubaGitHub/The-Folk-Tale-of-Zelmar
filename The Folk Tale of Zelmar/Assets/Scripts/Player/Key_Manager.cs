using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Manager : MonoBehaviour
{
    public int smallKey;
    public bool hasBigKey;

    UI_Controller uI_Controller_Script;
    [SerializeField] GameObject UICanvas;

    private void Awake()
    {
        uI_Controller_Script = UICanvas.GetComponent<UI_Controller>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        uI_Controller_Script.UpdateSmallKeyCount();

        if(hasBigKey)
        {
            uI_Controller_Script.bigKey.SetActive(true);
        }
    }
}
