using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public UnityEvent<int> onScoreChanged;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject);
        if (onScoreChanged == null) onScoreChanged = new UnityEvent<int>();
    }

    public void AddScore(int v)
    {
        score += v;
        onScoreChanged.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
        onScoreChanged.Invoke(score);
    }
}
