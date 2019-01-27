using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliensTrigger : MonoBehaviour
{
    [SerializeField]
    bool actionPerformed = false;

    PlayerController player;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.LogWarning("Checking Completeness");
        CheckForSecretRoom(col);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Debug.LogWarning("Checking Completeness");
        CheckForSecretRoom(col);
    }

    void CheckForSecretRoom(Collider2D col)
    {
        if (string.Compare(col.gameObject.tag, "Player", true) == 0)
        {
            if (player == null)
            {
                player = col.GetComponent<PlayerController>();
            }
            if(player != null && GameManager.Manager.IsGameComplete && !actionPerformed && player.IsGrounded && !player.IsMoving)
            {
                actionPerformed = true;
                GameManager.Manager.OnGoToSecretRoom.Invoke();
            }
            else
            {
                Debug.Log("COmplete: " + GameManager.Manager.IsGameComplete + " Action: " + actionPerformed + " Grounded: " + player.IsGrounded + " Moving: " + player.IsMoving);
            }
        }
    }
}
