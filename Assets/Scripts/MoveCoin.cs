using UnityEngine;

public class MoveCoin : MonoBehaviour
{   
    //Checks if the game is paused and moves the coin
    private void Update()
    {
        if (WorldMovement.Paused) 
        {
            return; 
        }

        transform.position += new Vector3(0, 0, -WorldMovement.Speed) * Time.deltaTime;
    }

    //Deletes the coin at the end of level and deletes it when the player touches it, updates the player score when that happens
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreUpdater.playerCoins += 1;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }    
    }
}
