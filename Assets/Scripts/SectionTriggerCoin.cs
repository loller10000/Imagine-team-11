using UnityEngine;

public class SectionTriggerCoin : MonoBehaviour
{
    public GameObject coin;
    [SerializeField] private float transformZ = 8; // NEW DEFAULT
    [SerializeField] private float randomX1 = 8; // NEW DEFAULT
    [SerializeField] private float randomX2 = 8; // NEW DEFAULT

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(coin, new Vector3(Random.Range(randomX1, randomX2), -1, transformZ), Quaternion.identity);
        }
    }
}
