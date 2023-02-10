using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_TitleScreen : MonoBehaviour
{
    public string firstSceneName;

    public GameObject chara;

    private void Update()
    {
        chara.transform.localRotation = Quaternion.Euler(0f, 0f,
            Mathf.Sin(Time.timeSinceLevelLoad * 2f) * 10f);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstSceneName);
    }
}
