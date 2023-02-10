using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_WaitFall : MonoBehaviour
{
    public float radius = 1f;

    protected Rigidbody2D rbody;
    protected bool triggered = false;

    protected virtual void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    protected virtual void FixedUpdate()
    {
        if (triggered) return;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < radius // within radius horizontally
            && player.transform.position.y < transform.position.y) // player is underneath
        {
            triggered = true;
            rbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
