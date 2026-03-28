using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private string targetScene;

    [SerializeField] private GameObject cutscene02;
    [SerializeField] private GameObject cutscene03;

    private void Awake()
    {
        cutscene02.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(StartButton());
    }

    private IEnumerator StartButton()
    {
        cutscene02.SetActive(false);
        cutscene03.SetActive(true);
        
        // TODO: Implement logic that plays Cutscene 3. After ~22 seconds.
        // TODO: Implement Cutscene 3 transitioning to Main Game Scene after ~15 sec.
        
        yield return new WaitForSeconds(15); 
        SceneLoader.LoadScene(targetScene);
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
