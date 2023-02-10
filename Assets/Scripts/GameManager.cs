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
        Invoke(nameof(ShowEndGame), 1f);
    }

    public void GameOver()
    {
        state = GameState.Lose;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.GetComponent<Player_Controller>().Die();
        Invoke(nameof(ShowEndGame), 1f);
    }

    private static int score = 0;

    /// <summary>
    /// Resets score
    /// </summary>
    public static void ResetScore() => score = 0;

    public static void AddScore(int point) => score = Mathf.Max(score + point, 0);

    public static int GetScore() => score;
}
