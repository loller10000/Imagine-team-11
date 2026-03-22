using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("World Settings")]
    public float worldSpeed = 10f;          // current speed of moving objects
    public float borderDistance = 50f;      // distance to travel before border

    [Header("References")]
    public PlayerController player;
    public GameObject borderUIPanel;        // Canvas with Continue/Quit buttons
    public Spawner spawner;

    [Header("Debug")]
    [SerializeField] private float distanceTraveled = 0f;
    [SerializeField] private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        borderUIPanel.SetActive(false);
        ResumeGame();
    }

    private void Update()
    {
        if (isPaused) return;

        // 1. Track distance
        distanceTraveled += worldSpeed * Time.deltaTime;

        // 2. Check border reached
        if (distanceTraveled >= borderDistance)
        {
            EnterBorder();
        }
    }

    private void EnterBorder()
    {
        isPaused = true;
        WorldMovement.Paused = true;

        player.enabled = false;
        borderUIPanel.SetActive(true);
    }

    /// <summary>
    /// Resets parameters & increases difficulty,
    /// </summary>
    public void ContinueGame()
    {
        distanceTraveled = 0f;
        worldSpeed += 2f;

        if (spawner != null) 
            spawner.NextVariant();

        WorldMovement.Paused = false;
        player.enabled = true;

        borderUIPanel.SetActive(false);
        isPaused = false;
    }

    public void EndGame()
    {
        // You can implement game over logic here
        Debug.Log("Game Over");
        Application.Quit();
    }

    private void ResumeGame()
    {
        distanceTraveled = 0f;
        WorldMovement.Paused = false;
        player.enabled = true;
        borderUIPanel.SetActive(false);
        isPaused = false;
    }
}