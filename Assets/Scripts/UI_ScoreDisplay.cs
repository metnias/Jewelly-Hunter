using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScoreDisplay : MonoBehaviour
{
    private Text display;

    void Start()
    {
        display = GetComponent<Text>();
    }

    void Update()
    {
        display.text = GameManager.GetScore().ToString("000000");
    }
}
