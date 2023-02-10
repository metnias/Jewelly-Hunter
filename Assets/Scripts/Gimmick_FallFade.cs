using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gimmick_FallFade : Gimmick_WaitFall
{
    private bool fallen = false;

    public float fadeTime = 0.5f;
    private float fadeTimer;
    private SpriteRenderer spr;

    protected override void Start()
    {
        base.Start();
        spr = GetComponent<SpriteRenderer>();
        fadeTimer = fadeTime;
    }

    private void Update()
    {
        if (!triggered || !fallen) return;
        fadeTimer -= Time.deltaTime;
        if(fadeTimer<= 0f) { gameObject.SetActive(false); return; }
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, fadeTimer / fadeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered) return; // not fallen
        if (collision.gameObject.layer != 6
            || collision.transform.position.y > transform.position.y) return; // not ground
        fallen = true;
    }
}