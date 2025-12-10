using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleData", menuName = "RunBeyond/CollectibleData")]
public class CollectibleData : ScriptableObject
{
    public GameObject collectiblePrefab;
    public int scoreValue = 10;
    public float spawnChance = 1f;
}
