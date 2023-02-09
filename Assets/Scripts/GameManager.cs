using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance() => _instance;

    private static GameManager _instance;

    private void Start()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
    }

    private void InitializeGame()
    {
        playing = true;
    }

    void Update()
    {
        
    }

    public bool playing;

    public void GameWin()
    {
        playing = false;
    }

    public void GameOver()
    {
        playing = false;
    }
}
