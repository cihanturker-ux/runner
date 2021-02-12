using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public Text mentosText;

    private int coin;

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("mentos score değeri");
        mentosText = coin.ToString();
    }

    public void AddScore(int amount)
    {
        coin += amount;
        mentosText = coin.ToString();


        PlayerPrefs.SetInt("mentos score değeri", coin);
    }
}
