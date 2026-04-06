using UnityEngine;

public class Deathzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: Make a playerStats class or smth to contain lives.
            Invoke(nameof(Restart), 2);
        }
    }

    // Reloads the scene after 2 seconds
    private void Restart()
    {
        SceneLoader.LoadScene("TestScene");
    }
}
