using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Overworld : MonoBehaviour
{

    float moveSpeed = 5f;


    public Overworld_Map_Points currentPoint;

    Animator playerAnim;

    string currentState;

    const string IDLE_O = "Player_Idle_Overworld";
    const string RIGHT_O = "Player_Right_Overworld";
    const string UP_O = "Player_Up_Overworld";
    const string DOWN_O = "Player_Down_Overworld";
    const string LEFT_O = "Player_Left_Overworld";

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Start()
    {
        ChangeAnimationState(IDLE_O);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPoint.transform.position) < .1f)
        {

            if (Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                ChangeAnimationState(RIGHT_O);

                if (currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }

            else if (Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                ChangeAnimationState(LEFT_O);

                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }


            else if (Input.GetAxisRaw("Vertical") > 0.5f)
            {
                ChangeAnimationState(UP_O);

                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }

            else if (Input.GetAxisRaw("Vertical") < -0.5f)
            {
                ChangeAnimationState(DOWN_O);

                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }
        }
    }

    void SetNextPoint(Overworld_Map_Points nextPoint)
    {
        currentPoint = nextPoint;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        playerAnim.Play(newState);

        currentState = newState;
    }
}
