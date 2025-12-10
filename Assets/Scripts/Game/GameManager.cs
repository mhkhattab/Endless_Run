using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState State { get; private set; }

    [Header("References")]
    public GameObject player;
    public Transform playerStart;
    public UIManager uiManager;

    [Header("Game")]
    public int score = 0;
    public float timeSurvived = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GoToMainMenu();
    }

    void Update()
    {
        if (State == GameState.Playing)
        {
            timeSurvived += Time.deltaTime;
            uiManager?.UpdateTime(timeSurvived);
        }
    }

    public void StartGame()
    {
        score = 0;
        timeSurvived = 0f;
        uiManager?.UpdateScore(score);
        uiManager?.HideAll();
        uiManager?.ShowHUD();
        State = GameState.Playing;
        // Position player
        if (player != null && playerStart != null)
            player.GetComponent<PlayerController>().ResetPlayer(playerStart.position, 1);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (State != GameState.Playing) return;
        State = GameState.Paused;
        Time.timeScale = 0f;
        uiManager?.ShowPause();
    }

    public void ResumeGame()
    {
        if (State != GameState.Paused) return;
        State = GameState.Playing;
        Time.timeScale = 1f;
        uiManager?.HidePause();
    }

    public void GameOver()
    {
        State = GameState.GameOver;
        Time.timeScale = 0f;
        uiManager?.ShowGameOver(score, timeSurvived);
    }

    public void AddScore(int amount)
    {
        score += amount;
        uiManager?.UpdateScore(score);
    }

    public void OnPlayerHitObstacle()
    {
        GameOver();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        State = GameState.MainMenu;
        Time.timeScale = 1f;
        uiManager?.ShowMainMenu();
    }

    public void Quit()
    {
        // In editor this does nothing; in build it quits.
        Application.Quit();
    }
}
