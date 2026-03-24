using UnityEngine;

public class MoveCoin : MonoBehaviour
{   
    
    private void Update()
    {
        if (WorldMovement.Paused) 
        {
            return; 
        }

        transform.position += new Vector3(0, 0, -WorldMovement.Speed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreUpdater.playerScore += 100;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }    
    }
}
