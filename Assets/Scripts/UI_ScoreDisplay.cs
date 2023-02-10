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
        display.text = GameManager.Instance().GetDisplayScore().ToString("000000");
    }
}
