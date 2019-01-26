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
        GameManager.Manager.OnUpdateScore.AddListener(UpdateScore);
        UpdateScore(0);
    }

    void UpdateScore(int newScore)
    {
        textBox.text = "Coins: " + newScore;
    }
}
