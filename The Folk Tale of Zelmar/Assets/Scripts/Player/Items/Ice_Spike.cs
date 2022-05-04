using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Spike : MonoBehaviour
{
    Rigidbody2D iceRb;

    [SerializeField] float speed;

    private void Awake()
    {
        iceRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        iceRb.velocity = transform.right * speed;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
