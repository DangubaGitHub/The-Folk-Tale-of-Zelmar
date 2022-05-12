using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Door : MonoBehaviour
{
    bool isClosed;
    bool isOpen;
    bool atDoor;
    public bool hasKey;

    Animator doorAnim;
    string currentState;

    const string CLOSE = "Door_Closed";
    const string OPEN = "Door_Open";

    Key_Manager key_Manager_Script;
    [SerializeField] GameObject Player;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
        key_Manager_Script = Player.GetComponent<Key_Manager>();
    }

    void Start()
    {
        atDoor = false;
        isClosed = true;
        ChangeAnimationState(CLOSE);
    }


    void Update()
    {
        if (atDoor)
        {
            if (Input.GetButtonDown("Use"))
            {
                if (isClosed && key_Manager_Script.smallKey > 0)
                {
                    isOpen = true;
                    isClosed = false;
                    ChangeAnimationState(OPEN);
                    key_Manager_Script.smallKey--;
                    Debug.Log("Door Opens");
                }

                else if(isClosed && key_Manager_Script.smallKey <= 0)
                {
                    Debug.Log("Door is Locked");
                }

                if (isOpen)
                {
                    if (Input.GetButtonDown("Enter"))
                    {
                        Debug.Log("Entering Door");
                        //SceneManager.LoadScene(???);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            atDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
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
