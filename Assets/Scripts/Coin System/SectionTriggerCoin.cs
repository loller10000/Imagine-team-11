using UnityEngine;

public class SectionTriggerCoin : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject coin;
    [SerializeField] private Transform[] lanePoints; // Replaces 'lanePositions[]'
    
    [Header("Spawn Settings")]
    [SerializeField] private float forwardOffset = 30; // Renamed from 'transformZ'
    [SerializeField] private CoinPattern[] patterns;
    
    // Method 0 Variables
    private float randomX1; // -3
    private float randomX2; // 3
    
    // Method 1-3 Variables
    private float[] laneXPosition = { -3f, 0f, 3f };
    private int coinsPerSpawn = 2;
    private Vector3[] lanePositions =
    {
        new (-3f, 4f, 0f), 
        new (0f, -1f, 0f), 
        new (3f, 4f, 0f)
    };
    
    // Uncomment to revert to original state of system. METHOD 0
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Trigger"))
    //     {
    //         Instantiate(coin, new Vector3(Random.Range(randomX1, randomX2), -1, transformZ), Quaternion.identity);
    //     }
    // }
    
    /// <summary>
    /// Pattern based coin spawning
    /// Configure these in the inspector
    /// Example: True, False, True would mean only coins spawning Left and Right.
    /// </summary>
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            // Safety checks
            if (patterns.Length == 0) return;
            if (lanePoints.Length == 0) return;
            
            // Picks a pattern at random
            var pattern = patterns[Random.Range(0, patterns.Length)];
            var laneCount = Mathf.Min(lanePoints.Length, pattern.lanes.Length);

            // Spawn Loop
            for (int i = 0; i < laneCount; i++)
            {
                if (pattern.lanes[i])
                {
                    var spawnPos = lanePoints[i].position + Vector3.forward * forwardOffset;
                    Instantiate(coin, spawnPos, Quaternion.identity);
                }
            }
            
            // METHOD 1: LANE-BASED SPAWNING
            // Spawns a coin in each lane.
            
            // foreach (float laneX in lanePositions)
            // {
            //     Instantiate(coin, new Vector3(laneX, -1, transformZ), Quaternion.identity);
            // }
            
            // METHOD 2: RANDOM LANES
            // Spawns X amount in a lane at random, could happen to be the same one.
            
            // for (int i = 0; i < coinsPerSpawn; i++)
            // {
            //     var lane =  lanePositions[Random.Range(0, lanePositions.Length)];
            //     Instantiate(coin, new Vector3(lane, -1, transformZ), Quaternion.identity);
            // }
            
            // METHOD 3: FIXED LANE POSITIONS
            // Uses hardcoded X positions, fixed Y.

            // for (int i = 0; i < laneCount; i++)
            // {
            //     Instantiate(coin,
            //     new Vector3(laneXPosition[i], -1f, forwardOffset),
            //     Quaternion.identity);
            // }
        }
    }
}
