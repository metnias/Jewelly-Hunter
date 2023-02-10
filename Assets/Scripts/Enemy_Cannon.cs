using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cannon : MonoBehaviour
{
    public GameObject shellPrefab;
    public float delayTime = 2f;
    public float triggerRadius = 10f;
    public float fireSpeedX = 20f;
    public float shellLife = 5f;

    private float waitTime = 0f;
    private GameObject player;
    private GameObject hole;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hole = transform.Find("Hole").gameObject;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance().Playing() || player == null) return;
        waitTime += Time.fixedDeltaTime;
        if (waitTime >= delayTime)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < triggerRadius)
            {
                waitTime = 0f;
                Vector3 pos = hole.transform.position;
                var shell = Instantiate(shellPrefab, pos, Quaternion.identity);
                var sBody = shell.GetComponent<Rigidbody2D>();
                sBody.AddForce(Mathf.Sign(transform.localScale.x) * fireSpeedX * Vector2.left, ForceMode2D.Impulse);
                Destroy(shell, shellLife);
            }
        }

    }
}
