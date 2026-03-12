using UnityEngine;

public class Move : MonoBehaviour
{
    [Range (1f, 100f)] [SerializeField] private float moveSpeedZ;
    
    /// <summary>
    /// Player feedback tot nu toe:
    /// Yntze: 10 lijkt me wel een goeie default
    /// Tom M.: Vindt het te sloom, lijkt eerder walking raad aan rond de ~20 instead.
    /// Ik ben (Jahva) ben het eens met deze statement is meer sprinting gevoel.
    /// </summary>

    void Update()
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
