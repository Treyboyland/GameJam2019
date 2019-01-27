using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    
    [SerializeField]
    AudioClip jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnPlayerJumped.AddListener(PlaySound);
    }

    void PlaySound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
}
