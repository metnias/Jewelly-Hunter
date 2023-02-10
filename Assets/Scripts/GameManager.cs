using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public GameObject buttonPanel;

    public static GameManager Instance() => _instance;

    private static GameManager _instance;

    private TimeController timeCtrler;

    private void Start()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        InitializeGame();
    }

    private void InitializeGame()
    {
        mainImage.transform.parent.gameObject.SetActive(true);
        mainImage.SetActive(true);
        buttonPanel.SetActive(false);
        state = GameState.Start;
        Invoke(nameof(StartGame), 1f);
    }

    private void StartGame()
    {
        mainImage.SetActive(false);
        state = GameState.Play;
        Time.timeScale = 1f;
    }

    private void ShowEndGame()
    {
        mainImage.SetActive(true);
        buttonPanel.SetActive(true);
        mainImage.GetComponent<UI_MainImage>().RefreshImage();
    }

    void Update()
    {

    }

    public bool Playing() => state == GameState.Play;
    public GameState GetState() => state;
    private GameState state;

    public enum GameState
    {
        Start,
        Play,
        Pause,
        Win,
        Lose
    }

    public void GameWin()
    {
        state = GameState.Win;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.GetComponent<Player_Controller>().Win();
        score += 100;
        Invoke(nameof(ShowEndGame), 1f);
    }

    public void GameOver()
    {
        state = GameState.Lose;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.GetComponent<Player_Controller>().Die();
        totalScore -= 50; if (totalScore < 0) totalScore = 0;
        Invoke(nameof(ShowEndGame), 1f);
    }

    #region Score

    private static int totalScore = 0;
    private int score = 0;

    public static void ResetTotalScore() => totalScore = 0;

    public void AddScore(int point) => score = Mathf.Max(score + point, 0);

    public int GetDisplayScore() => totalScore + score;

    public void AddTotalScore() => totalScore += Instance().score;

    #endregion Score
}
