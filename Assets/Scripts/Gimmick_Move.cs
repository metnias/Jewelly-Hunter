using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick_Move : MonoBehaviour
{
    public GameObject[] waypoints;

    public float pauseTime = 0.5f;
    public float speed = 2f;
    private int target;
    private float waitTime = 0f;

    void Start()
    {
        target = 0;
    }

    protected virtual void FixedUpdate()
    {
        if (!GameManager.Instance().Playing()) return;
        var p = waypoints[target];
        if (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0f)
            {
                target++;
                if (waypoints.Length <= target) target = 0;
            }
            return;
        }

        Vector3 rel = p.transform.position - transform.position;
        if (rel.magnitude > speed * Time.fixedDeltaTime) rel = rel.normalized * speed * Time.deltaTime;
        else if (rel.magnitude == 0f) waitTime = pauseTime;
        transform.Translate(rel);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        var rbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rbody == null || rbody.bodyType != RigidbodyType2D.Dynamic) return;
        collision.transform.SetParent(transform);
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.activeSelf && collision.transform.parent == transform)
            collision.transform.SetParent(null);
    }
}
