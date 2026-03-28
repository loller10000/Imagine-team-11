using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private string targetScene;
    
    public void StartGame()
    {
        StartCoroutine(StartButton());
    }

    private IEnumerator StartButton()
    {
        // TODO: Implement logic that plays Cutscene 3. After ~22 seconds.
        yield return new WaitForSeconds(3); 
        
        // TODO: Implement Cutscene 3 transitioning to Main Game Scene after ~15 sec.
        SceneLoader.LoadScene(targetScene); // Replace with string or SceneLoader class
    }

    public void SettingsButton()
    {
        // Open Panel etc.
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
