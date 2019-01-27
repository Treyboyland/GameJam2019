using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField]
    Vector2 offset;

    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        Vector3 playerPos = PlayerController.Player.transform.position;
        pos.x = playerPos.x + offset.x;
        pos.y = playerPos.y + offset.y;

        gameObject.transform.position = pos;
    }
}
