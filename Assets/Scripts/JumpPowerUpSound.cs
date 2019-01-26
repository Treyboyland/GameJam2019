using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUpSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnCollectedJumpPowerUp.AddListener(() => audioSource.PlayOneShot(audioSource.clip));
    }

}
