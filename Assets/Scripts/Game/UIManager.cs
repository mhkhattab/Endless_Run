using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public GameObject hud;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

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
    gameOverMenu.SetActive(true);

    finalScoreText.text = "Score: " + score;
    finalTimeText.text = "Time: " + time.ToString("F1") + "s";
}

    public void UpdateScore(int s)
    {
        if (scoreText) scoreText.text = s.ToString();
    }

    public void UpdateTime(float t)
    {
        if (timeText) timeText.text = $"{t:F1}s";
    }
    public void SyncVolumeSlider(UnityEngine.UI.Slider slider)
{
    if (AudioManager.Instance != null)
        slider.value = AudioManager.Instance.GetMusicVolume();
}
}
