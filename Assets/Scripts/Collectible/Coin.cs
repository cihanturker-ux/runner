using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Collectible()
    {
        FindObjectOfType<CoinManager>().AddScore(1);
    }
}
