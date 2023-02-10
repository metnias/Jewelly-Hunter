using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public float speed = 2f;
    private bool left = true;
    public LayerMask groundLayer; // Landable layer
    private Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance().Playing()) return;

        if (!Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer))
            left = !left; // turn

        transform.localScale = new Vector3(left ? 1f : -1f, 1f, 1f);
        rbody.velocity = new Vector2(left ? -speed : speed, rbody.velocity.y);
    }
}
