using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public static int playerCoins = 0;
    public TMP_Text score;

    // Update is called once per frame
    private void Update()
    {
        score.SetText(playerCoins.ToString());
    }
}
