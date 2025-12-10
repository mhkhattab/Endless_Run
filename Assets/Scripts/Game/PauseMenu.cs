using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (volumeSlider) volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    public void OnResumeButton()
    {
        GameManager.Instance?.ResumeGame();
    }

    public void OnMainMenuButton()
    {
        GameManager.Instance?.GoToMainMenu();
    }

    void OnVolumeChanged(float val)
    {
        AudioListener.volume = val;
    }
}
