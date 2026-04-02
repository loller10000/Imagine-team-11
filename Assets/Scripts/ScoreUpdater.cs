using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public static float playerCoins = 0;
    [SerializeField] public TMP_Text score;

    // Update is called once per frame
    void Update()
    {
        
        score.SetText(playerCoins.ToString());
    }
}
