using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    int value;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(string.Compare(col.gameObject.tag, "Player" , true) == 0)
        {
            GameManager.Manager.OnCollectedCoin.Invoke(value);
            Destroy(gameObject);
        } 
    }
}
