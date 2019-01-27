using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    [SerializeField]
    List<ParticleSystem> confettis;

    [SerializeField]
    GameObject oldHouse;

    [SerializeField]
    GameObject newHouse;

    bool doingConfetti;

    PlayerController player;

    void Start()
    {
        oldHouse.SetActive(true);
        newHouse.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.LogWarning("Checking");
        if(string.Compare(col.gameObject.tag, "Player", true) == 0)
        {
            if(player == null)
            {
                player = col.gameObject.GetComponent<PlayerController>();
            }
            if(player != null && !doingConfetti && GameManager.Manager.IsTargetSet && player.Score >= GameManager.Manager.TargetScore)
            {
                doingConfetti = true;
                DoWinningStuff();
            }
            else
            {
                Debug.LogWarning("Score: " + player.Score + ", Target: " + GameManager.Manager.TargetScore  +", Set: " + GameManager.Manager.IsTargetSet);
            }
        }
    }

    void DoWinningStuff()
    {
        foreach(ParticleSystem confetti in confettis)
        {
            confetti.Play();
        }
        newHouse.SetActive(true);
        oldHouse.SetActive(false);
        GameManager.Manager.OnGameComplete.Invoke();
    }
}
