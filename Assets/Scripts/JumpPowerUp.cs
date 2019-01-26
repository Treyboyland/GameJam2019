using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(string.Compare(col.gameObject.tag, "Player" , true) == 0)
        {
            GameManager.Manager.OnCollectedJumpPowerUp.Invoke();
            Destroy(gameObject);
        } 
    }
}
