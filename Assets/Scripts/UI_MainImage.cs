using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainImage : MonoBehaviour
{
    public Sprite gameOver;
    public Sprite gameClear;
    private Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    public void RefreshImage()
    {
        switch (GameManager.Instance().GetState())
        {
            default: return;
            case GameManager.GameState.Win:
                img.sprite = gameClear;
                break;
            case GameManager.GameState.Lose:
                img.sprite = gameOver;
                break;
        }
    }
}
