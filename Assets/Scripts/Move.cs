using UnityEngine;

public class Move : MonoBehaviour
{
    [Range (1f, 100f)] [SerializeField] private float moveSpeedZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -moveSpeedZ) * Time.deltaTime;
    }
}
