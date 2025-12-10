using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnStartButton()
    {
        GameManager.Instance?.StartGame();
    }

    public void OnQuitButton()
    {
        GameManager.Instance?.Quit();
    }
}
