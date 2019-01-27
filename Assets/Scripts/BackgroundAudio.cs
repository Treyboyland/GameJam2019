using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip normalBackground;

    [SerializeField]
    AudioClip winnerBackground;

    [SerializeField]
    AudioClip alienBackground;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnGameComplete.AddListener(SwapAudioWinner);
        GameManager.Manager.OnMuteBackgroundAudio.AddListener(() => audioSource.mute = true);
        GameManager.Manager.OnPlayAlienTheme.AddListener(SwapAudioAlien);
        SwapAudioNormal();
    }


    void SwapAudioNormal()
    {
        audioSource.clip = normalBackground;
        audioSource.mute = false;
        audioSource.Play();
    }

    void SwapAudioWinner()
    {
        audioSource.clip = winnerBackground;
        audioSource.mute = false;
        audioSource.Play();
    }

    void SwapAudioAlien()
    {
        audioSource.clip = alienBackground;
        audioSource.mute = false;
        audioSource.Play();
    }
}
