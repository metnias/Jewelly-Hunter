using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Item : MonoBehaviour
{
    public int score = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        GameManager.Instance().AddScore(score);
        gameObject.SetActive(false);
    }
}
