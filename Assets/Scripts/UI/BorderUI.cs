using UnityEngine;

public class BorderUI : MonoBehaviour
{
    public static BorderUI Instance;

    public GameObject successPanel;
    public GameObject failPanel;
    public GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowSuccess(int requiredCoins)
    {
        successPanel.SetActive(true);
        failPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ShowFailRetry(int requiredCoins, int lives)
    {
        successPanel.SetActive(false);
        failPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}