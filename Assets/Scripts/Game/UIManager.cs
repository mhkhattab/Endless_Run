using UnityEngine;
using UnityEngine.UI;
using text = UnityEngine.UI.Text;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public GameObject hud;
    public Text scoreText;
    public Text timeText;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public Text finalScoreText;
    public Text finalTimeText;

    void Start()
    {
        HideAll();
    }

    public void HideAll()
    {
        hud?.SetActive(false);
        mainMenu?.SetActive(false);
        pauseMenu?.SetActive(false);
        gameOverMenu?.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAll();
        mainMenu?.SetActive(true);
    }

    public void ShowHUD()
    {
        hud?.SetActive(true);
    }

    public void ShowPause()
    {
        pauseMenu?.SetActive(true);
    }

    public void HidePause()
    {
        pauseMenu?.SetActive(false);
    }

    public void ShowGameOver(int score, float time)
    {
        HideAll();
        gameOverMenu?.SetActive(true);
        if (finalScoreText) finalScoreText.text = $"Score: {score}";
        if (finalTimeText) finalTimeText.text = $"Time: {time:F1}s";
    }

    public void UpdateScore(int s)
    {
        if (scoreText) scoreText.text = s.ToString();
    }

    public void UpdateTime(float t)
    {
        if (timeText) timeText.text = $"{t:F1}s";
    }
}
