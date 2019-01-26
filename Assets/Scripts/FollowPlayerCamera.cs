using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        Vector3 playerPos = PlayerController.Player.transform.position;
        pos.x = playerPos.x;
        pos.y = playerPos.y;

        gameObject.transform.position = pos;
    }
}
