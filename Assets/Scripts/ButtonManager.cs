using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject restartButton;

    private void Update()
    {
        if (GameManager.Instance().GetState() == GameManager.GameState.Lose)
        {
            nextButton.GetComponent<Button>().interactable = false;
        }
    }

    public string nextSceneName;

    public void RequestRestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RequestNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
