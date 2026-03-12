using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject roadSection;
    [SerializeField] private float transformZ = 8; // NEW DEFAULT

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(roadSection, new Vector3(0, 0, transformZ), Quaternion.identity);
        }
    }
}
