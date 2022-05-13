using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Door : MonoBehaviour
{
    bool isClosed;
    bool isOpen;
    [SerializeField] bool atDoor;

    Animator doorAnim;
    string currentState;

    const string OPEN = "Boss_Door_Open";

    Key_Manager key_Manager_Script;
    [SerializeField] GameObject Player;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
        key_Manager_Script = Player.GetComponent<Key_Manager>();
    }

    void Start()
    {
        isClosed = true;
    }

    void Update()
    {
        if(atDoor)
        {
            if(Input.GetButtonDown("Use"))
            {
                if(isClosed && key_Manager_Script.hasBigKey)
                {
                    isOpen = true;
                    isClosed = false;
                    ChangeAnimationState(OPEN);
                    Debug.Log("Door Opens");
                }
                else if(isClosed && !key_Manager_Script.hasBigKey)
                {
                    Debug.Log("Door is Locked, you need a BIG KEY!");
                }
            }

            if(Input.GetButtonDown("Enter") && isOpen)
            {
                Debug.Log("Entering Door");
                //SceneManager.LoadScene(???);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            atDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            atDoor = false;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        doorAnim.Play(newState);

        currentState = newState;
    }
}
