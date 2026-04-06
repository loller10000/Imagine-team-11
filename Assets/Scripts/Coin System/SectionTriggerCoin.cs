using UnityEngine;

public class SectionTriggerCoin : MonoBehaviour
{
    public GameObject coin;
    [SerializeField] private float transformZ = 8; // NEW DEFAULT
    [SerializeField] private float randomX1 = 8; // NEW DEFAULT
    [SerializeField] private float randomX2 = 8; // NEW DEFAULT
    
    // Variables for experimental new approaches
    [SerializeField] private float[] lanePositions = { -3f, 0f, 3f };
    [SerializeField] private int coinsPerSpawn = 2;
    [SerializeField] private CoinPattern[] patterns;
    
    // Uncomment to revert to original state of system.
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
            CoinPattern pattern = patterns[Random.Range(0, patterns.Length)];

            for (int i = 0; i < lanePositions.Length; i++)
            {
                if (pattern.lanes[i])
                {
                    Instantiate(coin, new Vector3(lanePositions[i], -1, transformZ), Quaternion.identity);
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
        }
    }
}
