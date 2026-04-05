using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private GameObject playerAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thePlayer.GetComponent<PlayerController>().enabled = false;
            playerAnim.GetComponent<Animator>().SetTrigger("obstacleHit");

            WorldMovement.Paused = true;
            Invoke(nameof(Restart), 3);
        }
        else if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }    
    }

    private void Restart()
    {
        SceneLoader.LoadScene("TestScene");
    }
}
