using UnityEngine;

/// <summary>
/// Automatically spawns sections ahead of the player to create an endless road.
/// Sections are placed one after another, with optional gaps controlled by gapSize.
/// </summary>

public class Spawner : MonoBehaviour
{
    [Header("Variants")]
    public GameObject[] sectionPrefabs;   // different variants

    [Header("Spacing")]
    public float gapSize = 40f;           // How much distance between prefabA and prefabB?
    public float spawnDistance = 200f;    // Distance ahead of the player where new section spawns

    private int currentVariant = 0;
    private GameObject lastSpawned;

    private void Start()
    {
        SpawnSection(spawnDistance);
    }

    private void Update()
    {
        if (WorldMovement.Paused)
            return;

        if (!lastSpawned)
        {
            SpawnSection(spawnDistance);
            return;
        }

        var worldPosThreshold = lastSpawned.transform.position.z + gapSize;
        if (worldPosThreshold <= spawnDistance)
        {
            SpawnSection(worldPosThreshold);
        }
    }

    private void SpawnSection(float zPos)
    {
        if (sectionPrefabs.Length == 0) 
            return;

        var prefab = sectionPrefabs[currentVariant];
        var spawnPos = new Vector3(0, 0, zPos);
        lastSpawned = Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    // Called by GameManager after a border is passed.
    public void NextVariant()
    {
        currentVariant++;
        if (currentVariant >= sectionPrefabs.Length)
        {
            currentVariant = 0;
        }
    }
}