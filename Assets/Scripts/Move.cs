using UnityEngine;

public class Move : MonoBehaviour
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
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }    
    }
}
