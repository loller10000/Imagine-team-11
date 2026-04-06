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

    // Uncomment to revert to original state of system.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(coin, new Vector3(Random.Range(randomX1, randomX2), -1, transformZ), Quaternion.identity);
        }
    }
    
    // private void OnTriggerEnter(Collider other)
    // {
        // if (other.CompareTag("Trigger"))
        // {
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
        // }
    // }
}
