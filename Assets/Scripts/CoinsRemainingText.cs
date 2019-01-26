using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsRemainingText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textBox;


    int numCoins = 0;

    // Start is called before the first frame update
    void Start()
    {
        numCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        GameManager.Manager.OnCollectedCoin.AddListener((int a) => Subtract());
        SetCoinText();
    }

    void Subtract()
    {
        numCoins--;
        SetCoinText();
    }

    void SetCoinText()
    {
        textBox.text = "Coins Remaining: " + numCoins;
    }

}
