using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 10;
    public void Collect()
    {
        ScoreManager.Instance.AddScore(value);
        Destroy(gameObject);
    }
}
