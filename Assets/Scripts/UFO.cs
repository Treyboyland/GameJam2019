using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField]
    Animator beamAnimator;

    bool playerReady = false;

    [SerializeField]
    float secondsBeforeMove;

    [SerializeField]
    GameObject finalLocation;

    [SerializeField]
    GameObject maskObject;

    [SerializeField]
    AudioSource ufoSound;

    [SerializeField]
    float secondsToMoveCenter;

    [SerializeField]
    float secondsToMoveUp;

    [SerializeField]
    float rotateAtProgress;



    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnGoToSecretRoom.AddListener(() => playerReady = true);
        StartCoroutine(PerformAbduction());
    }

    IEnumerator MovePlayerToNewX()
    {
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();

        Vector3 pos = PlayerController.Player.gameObject.transform.position;

        while (timer.Elapsed.TotalSeconds < secondsToMoveCenter)
        {
            Vector3 newPos = PlayerController.Player.gameObject.transform.position;
            newPos.x = Mathf.Lerp(pos.x, finalLocation.transform.position.x, (float)timer.Elapsed.TotalSeconds / secondsToMoveCenter);
            PlayerController.Player.gameObject.transform.position = newPos;
            yield return null;
        }

        Vector3 finalPos = PlayerController.Player.transform.position;
        finalPos.x = finalLocation.transform.position.x;

        PlayerController.Player.transform.position = finalPos;

    }

    IEnumerator MovePlayerUp()
    {
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();

        Vector3 pos = PlayerController.Player.gameObject.transform.position;
        bool rotated = false;

        while (timer.Elapsed.TotalSeconds < secondsToMoveUp)
        {
            Vector3 newPos = PlayerController.Player.gameObject.transform.position;
            float progress = (float)timer.Elapsed.TotalSeconds / secondsToMoveUp;
            newPos.y = Mathf.Lerp(pos.y, finalLocation.transform.position.y, progress);

            if(!rotated && progress >= rotateAtProgress)
            {
                rotated = true;
                PlayerController.Player.Rotate();
            }

            PlayerController.Player.gameObject.transform.position = newPos;
            yield return null;
        }

        Vector3 finalPos = PlayerController.Player.transform.position;
        finalPos.y = finalLocation.transform.position.y;

        PlayerController.Player.transform.position = finalPos;
    }

    IEnumerator PerformAbduction()
    {

        while (!playerReady)
        {
            yield return null;
        }

        beamAnimator.SetTrigger("Beam Down");
        ufoSound.Play();
        GameManager.Manager.OnGoToSecretRoom.Invoke();
        GameManager.Manager.OnMuteBackgroundAudio.Invoke();

        while (!beamAnimator.GetCurrentAnimatorStateInfo(0).IsName("Finished"))
        {
            yield return null;
        }

        yield return StartCoroutine(MovePlayerToNewX());

        while (ufoSound.isPlaying)
        {
            yield return null;
        }


        GameManager.Manager.OnPlayAlienTheme.Invoke();

        yield return StartCoroutine(MovePlayerUp());


        //TODO: Move Player
    }
}
