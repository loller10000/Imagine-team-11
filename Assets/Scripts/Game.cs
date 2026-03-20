using System;
using TMPro;
using UnityEngine;

/// <summary>
/// This class is supposed to keep track of wether 
/// the game is playing or not.
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField] private Runner runner;
    [SerializeField] private TrackingCamera trackingCamera;
    [SerializeField] private TextMeshProUGUI displayText;

    [SerializeField] private float maxDeltaTime = 1f / 120f;

    private bool isPlaying;

    private void StartNewGame()
    {
        trackingCamera.StartNewGame();
        runner.StartNewGame();
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying)
        {
            UpdateGame();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter)) // Might change to spacebar.
        {
            StartNewGame();
        }
    }

    private void UpdateGame()
    {
        var accumulateDeltaTime = Time.deltaTime;
        while (accumulateDeltaTime > maxDeltaTime && isPlaying)
        {
            isPlaying = runner.Run(maxDeltaTime);
            accumulateDeltaTime -= maxDeltaTime;
        }

        isPlaying = isPlaying && runner.Run(accumulateDeltaTime);
        
        runner.UpdateVisualization();
        trackingCamera.Track(runner.Position);
        displayText.SetText("0", Mathf.Floor(runner.Position.z));
    }
}
