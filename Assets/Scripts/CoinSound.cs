using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnCollectedCoin.AddListener((int a) => audioSource.PlayOneShot(audioSource.clip));
    }
}
