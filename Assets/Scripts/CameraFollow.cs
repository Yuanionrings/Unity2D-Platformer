using System.Collections;
using System.Collections.Generic;
//using System.Media;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        //Camera Follow
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
