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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

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

    // -----------------------------------------------------------
    //                      GAME FLOW
    // -----------------------------------------------------------

    public void StartGame()
    {
        score = 0;
        timeSurvived = 0f;

        uiManager?.UpdateScore(score);
        uiManager?.HideAll();
        uiManager?.ShowHUD();

        State = GameState.Playing;
        Time.timeScale = 1f;

        // Reset and position player
        if (player != null && playerStart != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
                pc.ResetPlayer(playerStart.position, 1);
        }
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

    // -----------------------------------------------------------
    //                   PLAYER HIT FIXED VERSION
    // -----------------------------------------------------------

    public void OnPlayerHitObstacle()
    {
        Debug.Log("GAME OVER — Enemy caught the player!");

        // Stop player movement
        PlayerController pc = FindObjectOfType<PlayerController>();
        if (pc != null)
            pc.enabled = false;

        // Trigger real gameover flow (UI + freeze)
        GameOver();
    }

    // -----------------------------------------------------------
    //             RESTART / MENU / QUIT
    // -----------------------------------------------------------

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
        Application.Quit();
    }
}
