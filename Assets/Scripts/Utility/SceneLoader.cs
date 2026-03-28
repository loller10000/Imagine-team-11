using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] [Tooltip("Go to:")] private string sceneName;
    [SerializeField] private string currentSceneName;

    // Persistant instance of this GameObject across scenes.
    private static SceneLoader Instance { get; set; }

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        print(currentSceneName);
        Time.timeScale = 1;
    }

    private void Update()
    {
        
        
        // if (Input.GetKey(KeyCode.Backspace))
        // {
        //     ReloadCurrentScene();
        // }
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneName);
    }
    
}