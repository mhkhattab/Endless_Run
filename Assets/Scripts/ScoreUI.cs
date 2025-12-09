using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private int lastScore = -1;

    private void Update()
    {
        // Only update score internally, no UI text needed right now
        if (ScoreManager.Instance.score != lastScore)
        {
            lastScore = ScoreManager.Instance.score;
        }
    }
}
