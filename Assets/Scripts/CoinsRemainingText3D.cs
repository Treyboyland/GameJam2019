using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsRemainingText3D : MonoBehaviour
{
    [SerializeField]
    TextMeshPro textBox;


    int numCoins = 0;

    // Start is called before the first frame update
    void Start()
    {
        numCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        GameManager.Manager.OnTargetScoreSet.Invoke(numCoins);
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
        textBox.text = "" + numCoins;
    }

}
