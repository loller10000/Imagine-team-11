using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    public static bool isPaused;
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
    
    public void ResumeButton()
    {
        Time.timeScale = 1;
        container.SetActive(false);
        isPaused = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        container.SetActive(true);
        isPaused = true;
    }

    // Won't work in editor, should in standalone build.
    public void QuitGame()
    {
        Application.Quit();
    }
    
    // I'll handle this in another class.
    //public void MainMenuButton()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    //}
}
