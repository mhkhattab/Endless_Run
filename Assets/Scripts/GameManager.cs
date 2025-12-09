using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public GameObject gameOverUI;
    public GameObject pauseUI;

    [Header("State")]
    public bool isGameOver = false;
    public float elapsedTime = 0f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void PlayerDied()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        elapsedTime = 0f;

        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        elapsedTime = 0f;

        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        if (isGameOver) return;

        Time.timeScale = 0f;

        if (pauseUI != null)
            pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (pauseUI != null)
            pauseUI.SetActive(false);
    }
}
