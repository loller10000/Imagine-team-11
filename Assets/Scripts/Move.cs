using UnityEngine;

public class Move : MonoBehaviour
{
    [Range (1f, 100f)] [SerializeField] private float moveSpeedZ;
    
    /// <summary>
    /// Player feedback tot nu toe:
    /// Yntze: 10 lijkt me wel een goeie default, als of je aant lopen/joggen bent.
    /// Tom M.: Vindt het te sloom, lijkt eerder walking raad aan rond de ~20 instead.
    /// Andy: 40, nu rent ie zo snel als een auto lijkt het wel.
    /// Christian Roth: Increase speed so it becomes gradually faster
    /// And vary between platform size sometimes bigger, sometimes smaller.
    /// </summary>

    private void Update()
    {
        transform.position += new Vector3(0, 0, -moveSpeedZ) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }    
    }
}
