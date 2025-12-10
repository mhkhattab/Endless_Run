using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "RunBeyond/DifficultySettings")]
public class DifficultySettings : ScriptableObject
{
    public float initialSpawnInterval = 1.5f;
    public float minSpawnInterval = 0.5f;
    public float spawnAcceleration = 0.001f; // decrease interval over time
    public float forwardSpeedIncreasePerSecond = 0.02f;
}
