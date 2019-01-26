using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnCollectedCoin.AddListener(UpdateScore);
    }

    void UpdateScore(int newScore)
    {
        textBox.text = "" + newScore;
    }
}
