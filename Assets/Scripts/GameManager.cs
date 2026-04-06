using UnityEngine;

/// <summary>
/// Controls the game loop: distance tracking, border triggers,
/// difficulty scaling and notifies the spawner for a variant change.
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("World Settings")]
    public float worldSpeed = 10f;          // current speed of moving objects
    public float borderDistance = 50f;      // distance to travel before border

    [Header("Difficulty")]
    public float speedIncrementPerBorder = 1f;
    
    [Header("Lives")]
    public int maxLives = 3;
    private float coinsAtSectionStart;

    [Header("Toll")] [Tooltip("Base coins required for the first border. Subsequent borders increase by this amount.")]
    public int baseToll = 10;
    public int tollIncrement = 10;          // each next border costs +10

    [Header("References")]
    public PlayerController player;
    public GameObject borderUIPanel;
    public Spawner spawner;

    [Header("Debug")]
    [SerializeField] private float distanceTraveled = 0f;
    [SerializeField] private int bordersPassed = 0;
    [SerializeField] private int currentLives;
    [SerializeField] private bool isPaused = false;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentLives = maxLives;
        
        WorldMovement.Speed = worldSpeed;   // Sync at start
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
        // If player is not grounded, wait until he is to continue.
        if(player.Gcc == 0)
            return;
        
        isPaused = true;
        WorldMovement.Paused = true;
        player.enabled = false;
        borderUIPanel.SetActive(true);
        
        var requiredCoins = baseToll + (bordersPassed * tollIncrement);
        if (ScoreUpdater.playerCoins >= requiredCoins)
        {
            BorderUI.Instance.ShowSuccess(requiredCoins);
        }
        else
        {
            if (currentLives > 0)
            {
                BorderUI.Instance.ShowFailRetry(requiredCoins, currentLives);
            }
            else
            {
                BorderUI.Instance.ShowGameOver();
            }
        }
    }

    /// <summary>
    /// Resets parameters & increases difficulty,
    /// </summary>
    public void ContinueGame()
    {
        // Deduct the toll for this border
        var requiredCoins = baseToll + (bordersPassed * tollIncrement);
        ScoreUpdater.playerCoins -= requiredCoins;
        Debug.Log($"Paid {requiredCoins} coins, Remaining: {ScoreUpdater.playerCoins}");
        
        bordersPassed++;
        coinsAtSectionStart = ScoreUpdater.playerCoins;
        
        distanceTraveled = 0f;
        worldSpeed += speedIncrementPerBorder;
        WorldMovement.Speed = worldSpeed;   // Sync after increase

        spawner?.NextVariant();

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
        bordersPassed = 0;
        WorldMovement.Paused = false;
        player.enabled = true;
        borderUIPanel.SetActive(false);
        isPaused = false;
    }
    
    public void RetrySection()
    {
        currentLives--;

        // Reset coins to checkpoint - No need to, too diff i think.
        // ScoreUpdater.playerCoins = coinsAtSectionStart;

        // Reset distance so player replays section
        distanceTraveled = 0f;

        WorldMovement.Paused = false;
        player.enabled = true;

        borderUIPanel.SetActive(false);
        isPaused = false;
    }

    public void RetryNewGame()
    {
        SceneLoader.LoadScene("TestScene");
    }

    // TODO: Add more worldSpeed syncs.
    // For example increase per coin picked up:
    // Would probably require a sync statement in the Update().
}