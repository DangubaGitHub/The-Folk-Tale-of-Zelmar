using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Ball : MonoBehaviour
{
    Rigidbody2D ballRb;
    Animator ballAnim;

    string currentState;
    const string BALL_DESTROY = "FireBall_Destroy";

    [SerializeField] float force;

    private void Awake()
    {
        ballRb = GetComponent<Rigidbody2D>();
        ballAnim = GetComponent<Animator>();
    }

    void Start()
    {
        ballRb.velocity = transform.right * force;
        StartCoroutine(DelayDestroy());
        Invoke("DestroyBall", 3.2f);
    }

    void Update()
    {
        
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2);
        ChangeAnimationState(BALL_DESTROY);
    }

    void DestroyBall()
    {
        Destroy(gameObject);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        ballAnim.Play(newState);

        currentState = newState;
    }
}
