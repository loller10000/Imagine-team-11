using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    [SerializeField] private bool isPaused;
    // [SerializeField] private string sceneName;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeButton();
            }

            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        container.SetActive(true);
        isPaused = true;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        container.SetActive(false);
        isPaused = false;
    }
    
    // I'll handle this in another class.
    //public void MainMenuButton()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    //}
}
