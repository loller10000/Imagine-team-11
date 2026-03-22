using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = WorldMovement.Speed;
    
    private void Update()
    {
        if (WorldMovement.Paused) 
        {
            return; 
        }

        transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }    
    }
}
