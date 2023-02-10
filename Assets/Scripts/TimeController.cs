using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;
    public float gameTime = 0f;
    public bool isTimeOver = false;
    public float displayTime = 0f;
    public GameObject timer;

    private Text timerText;
    private float curTime = 0f;

    void Start()
    {
        if (gameTime == 0f) gameObject.SetActive(false);

        if (isCountDown) displayTime = gameTime;
        isTimeOver = false;
        curTime = 0f;
        timerText = timer.GetComponent<Text>();
    }

    void Update()
    {
        if (GameManager.Instance().GetState() != GameManager.GameState.Play)
        {
            if (GameManager.Instance().GetState() == GameManager.GameState.Start) RefreshTimer();
            return;
        }
        if (isTimeOver) { GameManager.Instance().GameOver(); return; }
        curTime += Time.deltaTime;
        if (isCountDown)
        {
            displayTime = gameTime - curTime;
            if (displayTime <= 0f)
            {
                displayTime = 0f;
                isTimeOver = true;
            }
        }
        else
        {
            displayTime = curTime;
            if (displayTime >= gameTime)
            {
                displayTime = gameTime;
                isTimeOver = true;
            }
        }
        RefreshTimer();
    }

    public void RefreshTimer()
    {
        timerText.text = (displayTime * 5f).ToString("0");
    }
}
