using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    Vector2 playerMin;

    [SerializeField]
    Vector2 playerMax;

    [SerializeField]
    Vector2 backgroundMin;

    [SerializeField]
    Vector2 backgroundMax;


    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Player;
    }

    // Update is called once per frame
    void Update()
    {
        SetNewPostition();
    }

    void SetNewPostition()
    {
        Vector2 newPos = new Vector2();
        Vector2 playerRange;
        playerRange.x = playerMax.x - playerMin.x;
        playerRange.y = playerMax.y - playerMin.y;

        newPos.x = Mathf.LerpUnclamped(backgroundMin.x, backgroundMax.x , Mathf.Abs(player.transform.position.x - playerMin.x) / playerRange.x);
        newPos.y = Mathf.LerpUnclamped(backgroundMin.y, backgroundMax.y, Mathf.Abs(player.transform.position.y - playerMin.y) / playerRange.y);

        gameObject.transform.position = newPos;
    }
}
