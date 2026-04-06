using UnityEngine;

public class CoinSpinner : MonoBehaviour
{
    //Spins the coin
    [SerializeField] public GameObject coin;
    void Update()
    {
        coin.transform.Rotate(0f, 0f, 1f);
    }
}
