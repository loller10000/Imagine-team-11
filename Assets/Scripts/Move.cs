using UnityEngine;

public class Move : MonoBehaviour
{   
    //Makes the platform move and checks if the game is paused or not
    private void Update()
    {
        if (WorldMovement.Paused) 
        {
            return; 
        }

        transform.position += new Vector3(0, 0, -WorldMovement.Speed) * Time.deltaTime;
    }

    //This destroys the walls at the end of the level for optimization
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }    
    }
}
