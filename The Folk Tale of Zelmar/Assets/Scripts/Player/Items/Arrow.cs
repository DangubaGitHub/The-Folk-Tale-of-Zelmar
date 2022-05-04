using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D arrowRb;

    [SerializeField] float speed;

    private void Awake()
    {
        arrowRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        arrowRb.velocity = transform.right * speed;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
